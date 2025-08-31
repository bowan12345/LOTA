using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOTA.DataAccess.Repository;
using LOTA.Model.DTO.Admin;
using LOTA.Model;
using LOTA.Service.Service.IService;
using LOTA.DataAccess.Repository.IRepository;

namespace LOTA.Service.Service
{
    public class LOResultService : ILOResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LOResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TrimesterCourse>> GetLatestTrimesterCourseOfferingsAsync()
        {
            try
            {
                return await _unitOfWork.trimesterCourseRepository.GetLatestTrimesterCourseOfferingsAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get latest trimester course offerings");
            }
        }

        public async Task<LOResultDTO> GetLOResultsByCourseOfferingAsync(string courseOfferingId)
        {
            try
            {

                // Get the course offering with related data
                var courseOffering = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseWithDetailsAsync(courseOfferingId);
                if (courseOffering == null)
                {
                    throw new InvalidOperationException("Course offering not found");
                }

                // Get the qualification
                if (courseOffering.Course == null)
                {
                    throw new InvalidOperationException("Course information not found for course offering");
                }
                
                var qualification = courseOffering.Course.Qualification;
                if (qualification == null)
                {
                    // Try to get qualification directly from database
                    var course = await _unitOfWork.courseRepository.GetByIdAsync(courseOffering.CourseId, "Qualification");
                    if (course?.QualificationId != null)
                    {
                        qualification = course.Qualification ?? await _unitOfWork.qualificationRepository.GetByIdAsync(course.QualificationId);
                    }
                    
                    if (qualification == null)
                    {
                        throw new InvalidOperationException($"Qualification information not found for course ");
                    }
                }
                
                var qualificationResult = new LOResultDTO
                {
                    QualificationId = qualification.Id,
                    QualificationName = qualification.QualificationName,
                    CourseOfferings = new List<CourseOfferingResultDTO>()
                };

                var courseOfferingResult = new CourseOfferingResultDTO
                {
                    CourseOfferingId = courseOffering.Id,
                    CourseName = courseOffering.Course.CourseName,
                    CourseCode = courseOffering.Course.CourseCode,
                    TrimesterName = $"Trimester {courseOffering.Trimester.TrimesterNumber} {courseOffering.Trimester.AcademicYear}",
                    Students = new List<StudentResultDTO>()
                };

                // Get students enrolled in this course offering
                var enrolledStudents = await _unitOfWork.studentCourseRepository.GetByCourseOfferingIdAsync(courseOffering.Id);

                // Use optimized batch processing method
                try
                {
                    var studentResults = await GetStudentResultsBatchAsync(enrolledStudents, courseOffering.Id);
                    courseOfferingResult.Students.AddRange(studentResults);
                }
                catch (Exception ex)
                {
                    // Log the error and throw with detailed message
                    Console.WriteLine($"Batch processing failed: {ex.Message}");
                    throw new InvalidOperationException($"Failed to retrieve student results using batch processing");
                }

                qualificationResult.CourseOfferings.Add(courseOfferingResult);
                return qualificationResult;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get LO results by course offering");
            }
        }

        
        public async Task<List<RetakeHistoryDTO>> GetRetakeHistoryAsync(string studentId, string learningOutcomeName)
        {
            try
            {
                var retakeHistory = new List<RetakeHistoryDTO>();
                
                // Get all LO scores for this student and learning outcome
                var allScores = await _unitOfWork.studentLOScoreRepository
                    .GetByStudentAndLearningOutcomeAsync(studentId, learningOutcomeName);
                
                // Group by assessment to show history
                var groupedScores = allScores.GroupBy(s => s.StudentAssessmentScore.AssessmentId);
                
                foreach (var group in groupedScores)
                {
                    var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(group.Key);
                    if (assessment == null) continue;
                    
                    var scores = group.OrderBy(s => s.CreatedDate).ToList();
                    
                    // Find original score (first one)
                    var originalScore = scores.FirstOrDefault();
                    
                    // Find latest active score
                    var latestActiveScore = scores.LastOrDefault(s => s.IsActive);
                    
                    if (originalScore != null && latestActiveScore != null)
                    {
                        var learningOutcome = assessment.AssessmentLearningOutcomes
                            .FirstOrDefault(alo => alo.LearningOutcome.LOName == learningOutcomeName);
                        
                        if (learningOutcome != null)
                        {
                            retakeHistory.Add(new RetakeHistoryDTO
                            {
                                AssessmentName = assessment.AssessmentName,
                                OriginalScore = originalScore.Score,
                                RetakeScore = latestActiveScore.Score,
                                MaxScore = learningOutcome.Score,
                                RetakeDate = latestActiveScore.CreatedDate ?? DateTime.UtcNow,
                                IsActive = latestActiveScore.IsActive
                            });
                        }
                    }
                }
                
                return retakeHistory.OrderByDescending(r => r.RetakeDate).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get retake history");
            }
        }
        

        
        public async Task<object> GetRetakeDataByAssessmentIdsAsync(string studentId, string courseOfferingId, List<string> assessmentIds)
        {
            try
            {
                var failedAssessments = new List<object>();
                string learningOutcomeName = null;
                
                foreach (var assessmentId in assessmentIds)
                {
                    // Get the assessment
                    var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(assessmentId);
                    if (assessment == null)
                    {
                        continue;
                    }
                    
                    // Get student's score for this assessment
                    var studentAssessmentScore = await _unitOfWork.studentScoreRepository
                        .GetStudentScoreByStudentAssessmentAsync(studentId, assessmentId);
                    
                    if (studentAssessmentScore == null)
                    {
                        continue;
                    }
                    
                    // Get all student LO scores for this assessment
                    var studentLOScores = await _unitOfWork.studentLOScoreRepository
                        .GetStudentLOScoresByStudentAssessmentScoreAsync(studentAssessmentScore.Id);
                    
                    foreach (var studentLOScore in studentLOScores)
                    {
                        // Get the assessment learning outcome
                        var assessmentLearningOutcome = assessment.AssessmentLearningOutcomes
                            .FirstOrDefault(assessmentLearningOutcome => assessmentLearningOutcome.Id == studentLOScore.AssessmentLearningOutcomeId);
                        
                        if (assessmentLearningOutcome == null)
                        {
                            continue;
                        }
                        
                        // Set the learning outcome name (should be the same for all assessments)
                        if (learningOutcomeName == null)
                        {
                            learningOutcomeName = assessmentLearningOutcome.LearningOutcome.LOName;
                        }
                        
                        var percentage = (studentLOScore.Score / assessmentLearningOutcome.Score) * 100;
                        
                        // Only include failed assessments (below 50%)
                        if (percentage < 50)
                        {
                            failedAssessments.Add(new
                            {
                                AssessmentId = assessment.Id,
                                AssessmentName = assessment.AssessmentName,
                                LOScore = studentLOScore.Score,
                                MaxLOScore = assessmentLearningOutcome.Score,
                                LOPercentage = percentage
                            });
                        }
                    }
                }
                
                return new
                {
                    failedAssessments = failedAssessments,
                    loName = learningOutcomeName ?? "Unknown LO"
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get retake data ");
            }
        }

        private async Task<StudentResultDTO> GetStudentResultAsync(string studentId, string courseOfferingId)
        {
            try
            {
                var student = await _unitOfWork.studentRepository.GetByIdAsync(studentId);
                if (student == null) return null;

                var courseOffering = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseWithDetailsAsync(courseOfferingId);
                if (courseOffering == null) return null;

                // Get assessments for this course using existing repository method
                var assessments = await _unitOfWork.assessmentRepository.GetAssessmentsByCourseOfferingId(courseOfferingId);
                var courseAssessments = assessments.Where(a => a.CourseOfferingId == courseOfferingId);

                var studentResult = new StudentResultDTO
                {
                    StudentId = student.Id,
                    StudentName = $"{student.FirstName} {student.LastName}",
                    StudentNo = student.StudentNo,
                    StudentEmail = student.Email,
                    Assessments = new List<AssessmentResultDTO>(),
                    TotalScore = 0,
                    MaxTotalScore = 0
                };

                foreach (var assessment in courseAssessments)
                {
                    var assessmentResult = new AssessmentResultDTO
                    {
                        AssessmentId = assessment.Id,
                        AssessmentName = assessment.AssessmentName,
                        MaxAssessmentScore = assessment.Score,
                        Weight = assessment.Weight,
                        LearningOutcomes = new List<LearningOutcomeResultDTO>()
                    };

                    // Get assessment learning outcomes using existing repository method
                    var assessmentLOs = await _unitOfWork.assessmentRepository.GetLOListByAssessmentId(assessment.Id, "LearningOutcome");
                    
                    foreach (var assessmentLO in assessmentLOs)
                    {
                        // Check if LearningOutcome is loaded
                        if (assessmentLO.LearningOutcome == null)
                        {
                            continue; // Skip this LO if LearningOutcome is not loaded
                        }
                        
                        var loResult = new LearningOutcomeResultDTO
                        {
                            LearningOutcomeId = assessmentLO.LOId,
                            LearningOutcomeName = assessmentLO.LearningOutcome.LOName,
                            AssessmentLearningOutcomeId = assessmentLO.Id,
                            MaxLOScore = assessmentLO.Score
                        };

                        // Get historical scores for this LO (IsActive = false)
                        try
                        {
                            var historicalScores = await _unitOfWork.studentLOScoreRepository.GetAllAsync(lo=>lo.IsActive == false&& lo.AssessmentLearningOutcomeId == assessmentLO.Id);
                            if (historicalScores != null)
                            {
                                if (historicalScores.ToList().Any())
                                {
                                    loResult.HistoricalScores = historicalScores.Select(hs => new HistoricalScoreDTO
                                    {
                                        Score = hs.Score,
                                        MaxScore = hs.AssessmentLearningOutcome?.Score ?? 0,
                                        Date = hs.CreatedDate ?? DateTime.Now
                                    }).ToList();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException($"Warning: Failed to get historical scores for LO");
                        }

                        // Get student's score for this LO - we need to check if StudentAssessmentScore exists first
                        var studentAssessmentScore = await _unitOfWork.studentScoreRepository.GetStudentScoreByStudentAssessmentAsync(studentId, assessment.Id);
                        
                        if (studentAssessmentScore != null)
                        {
                            // Get LO scores for this student assessment
                            var studentLOScores = await _unitOfWork.studentLOScoreRepository.GetStudentLOScoresByStudentAssessmentScoreAsync(studentAssessmentScore.Id);
                            var studentScore = studentLOScores.FirstOrDefault(sls => sls.AssessmentLearningOutcomeId == assessmentLO.Id);

                            if (studentScore != null)
                            {
                                loResult.LOScore = studentScore.Score;
                                loResult.LOPercentage = (studentScore.Score / assessmentLO.Score) * 100;
                                loResult.LOPassed = loResult.LOPercentage >= 50;
                                loResult.NeedsRetake = !loResult.LOPassed;
                                
                                // Check if this LO has retake history and populate retake data
                                var allScoresForLO = await _unitOfWork.studentLOScoreRepository
                                    .GetStudentLOScoresByStudentAssessmentScoreAsync(studentAssessmentScore.Id);
                                var loScores = allScoresForLO.Where(s => s.AssessmentLearningOutcomeId == assessmentLO.Id);
                                
                                // Check for retake data
                                var retakeScore = loScores.FirstOrDefault(s => s.IsRetake);
                                if (retakeScore != null)
                                {
                                    loResult.IsRetake = true;
                                    loResult.RetakeDate = retakeScore.RetakeDate;
                                    // Calculate if retake passed (score >= 50% of max score)
                                    var retakePercentage = (retakeScore.Score / assessmentLO.Score) * 100;
                                    loResult.RetakePassed = retakePercentage >= 50;
                                    loResult.RetakeFailed = retakePercentage < 50;
                                }
                                
                                loResult.HasRetake = loScores.Count(s => !s.IsActive) > 0;
                            }
                            else
                            {
                                loResult.LOScore = 0;
                                loResult.LOPercentage = 0;
                                loResult.LOPassed = false;
                                loResult.NeedsRetake = true;
                                loResult.HasRetake = false;
                            }
                        }
                        else
                        {
                            loResult.LOScore = 0;
                            loResult.LOPercentage = 0;
                            loResult.LOPassed = false;
                            loResult.NeedsRetake = true;
                        }

                        assessmentResult.LearningOutcomes.Add(loResult);
                    }

                    // Calculate assessment totals
                    assessmentResult.AssessmentScore = assessmentResult.LearningOutcomes.Sum(lo => lo.LOScore);
                    assessmentResult.AssessmentPercentage = (assessmentResult.AssessmentScore / assessmentResult.MaxAssessmentScore) * 100;
                    assessmentResult.AssessmentPassed = assessmentResult.AssessmentPercentage >= 50;

                    studentResult.Assessments.Add(assessmentResult);
                }

                // Set HasFailed and FailedAssessments for each LO
                foreach (var assessment in studentResult.Assessments)
                {
                    foreach (var lo in assessment.LearningOutcomes)
                    {
                        if (!lo.LOPassed)
                        {
                            lo.HasFailed = true;
                            lo.FailedAssessments.Add(new FailedAssessmentDTO
                            {
                                AssessmentId = assessment.AssessmentId,
                                AssessmentName = assessment.AssessmentName,
                                LOScore = lo.LOScore,
                                MaxLOScore = lo.MaxLOScore,
                                LOPercentage = lo.LOPercentage
                            });
                        }
                    }
                }

                // Calculate overall totals
                studentResult.MaxTotalScore = studentResult.Assessments.Sum(a => a.MaxAssessmentScore);
                studentResult.TotalScore = studentResult.Assessments.Sum(a => a.AssessmentScore);
                studentResult.TotalPercentage = (studentResult.TotalScore / studentResult.MaxTotalScore) * 100;
                studentResult.OverallPassed = studentResult.TotalPercentage >= 50;
                studentResult.NeedsResit = !studentResult.OverallPassed;

                return studentResult;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get student result");
            }
        }

        // New optimized batch processing method
        private async Task<List<StudentResultDTO>> GetStudentResultsBatchAsync(
            IEnumerable<StudentCourse> enrolledStudents, 
            string courseOfferingId)
        {
            try
            {
                var studentIds = enrolledStudents.Select(sc => sc.StudentId).ToList();
                
                // Batch retrieve all student basic information
                var students = await _unitOfWork.studentRepository.GetByIdsAsync(studentIds);
                
                // Use JOIN query to retrieve all assessments and learning outcomes at once
                var assessmentsWithLOs = await _unitOfWork.assessmentRepository.GetAssessmentsWithLOsByCourseOfferingId(courseOfferingId);
                
                // Validate that we have the required data
                if (!assessmentsWithLOs.Any())
                {
                    throw new InvalidOperationException("No assessments found for this course offering");
                }
                
                // Batch retrieve all student scores
                var allStudentScores = await _unitOfWork.studentScoreRepository.GetStudentScoresByCourseOfferingAsync(courseOfferingId);
                var allLOScores = await _unitOfWork.studentLOScoreRepository.GetLOScoresByCourseOfferingAsync(courseOfferingId);
                
                // Validate that we have student data
                if (!students.Any())
                {
                    throw new InvalidOperationException("No students found for this course offering");
                }

                // Validate batch data integrity before processing
                if (!ValidateBatchData(assessmentsWithLOs, allStudentScores, allLOScores))
                {
                    throw new InvalidOperationException("Batch data validation failed - data integrity issues detected");
                }

                var results = new List<StudentResultDTO>();

                // Build student results - Process data in memory to avoid N+1 queries
                foreach (var studentCourse in enrolledStudents)
                {
                    var student = students.FirstOrDefault(s => s.Id == studentCourse.StudentId);
                    if (student != null)
                    {
                        var studentResult = BuildStudentResultFromBatchData(
                            student, 
                            courseOfferingId, 
                            assessmentsWithLOs, 
                            allStudentScores, 
                            allLOScores);
                        
                        if (studentResult != null)
                        {
                            results.Add(studentResult);
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Batch processing failed: {ex.Message}");
            }
        }

        // Build student results using batch data to avoid N+1 queries
        private StudentResultDTO BuildStudentResultFromBatchData(
            ApplicationUser student, 
            string courseOfferingId, 
            IEnumerable<AssessmentWithLOsDTO> assessmentsWithLOs, 
            IEnumerable<StudentAssessmentScore> allStudentScores, 
            IEnumerable<StudentLOScore> allLOScores)
        {
            try
            {
                var studentResult = new StudentResultDTO
                {
                    StudentId = student.Id,
                    StudentName = $"{student.FirstName} {student.LastName}",
                    StudentNo = student.StudentNo,
                    StudentEmail = student.Email,
                    Assessments = new List<AssessmentResultDTO>(),
                    TotalScore = 0,
                    MaxTotalScore = 0
                };

                // Get ALL assessment scores for this student in this course offering
                var studentAssessmentScores = allStudentScores.Where(s => s.StudentId == student.Id).ToList();
                
                if (studentAssessmentScores.Any())
                {
                    foreach (var assessment in assessmentsWithLOs)
                    {
                        var assessmentResult = new AssessmentResultDTO
                        {
                            AssessmentId = assessment.AssessmentId,
                            AssessmentName = assessment.AssessmentName,
                            MaxAssessmentScore = assessment.MaxAssessmentScore,
                            Weight = assessment.Weight,
                            LearningOutcomes = new List<LearningOutcomeResultDTO>()
                        };

                        // Find the specific assessment score for this assessment
                        var studentAssessmentScore = studentAssessmentScores.FirstOrDefault(s => s.AssessmentId == assessment.AssessmentId);
                        
                        if (studentAssessmentScore != null)
                        {
                            // Get LO scores for this specific assessment
                            var studentLOScores = allLOScores.Where(s => s.StudentAssessmentScoreId == studentAssessmentScore.Id).ToList();

                            foreach (var assessmentLO in assessment.AssessmentLearningOutcomes)
                            {
                                var loResult = new LearningOutcomeResultDTO
                                {
                                    LearningOutcomeId = assessmentLO.LOId,
                                    LearningOutcomeName = assessmentLO.LearningOutcome.LOName,
                                    AssessmentLearningOutcomeId = assessmentLO.Id,
                                    MaxLOScore = assessmentLO.Score
                                };

                                // Find student score for this specific LO
                                // Get all score records for this LO, including retake scores
                                var allScoresForLO = studentLOScores.Where(s => s.AssessmentLearningOutcomeId == assessmentLO.Id).ToList();
                                
                                if (allScoresForLO.Any())
                                {
                                                    // According to business rules: IsActive=true and IsRetake=true means new score after retake
                // IsActive=true and IsRetake=false means old score
                                    
                                    // Prioritize new score after retake
                                    var retakeScore = allScoresForLO
                                        .Where(s => s.IsActive && s.IsRetake)
                                        .OrderByDescending(s => s.CreatedDate)
                                        .FirstOrDefault();
                                    
                                    // If no retake score exists, select the latest active score (old score)
                                    var activeScore = allScoresForLO
                                        .Where(s => s.IsActive && !s.IsRetake)
                                        .OrderByDescending(s => s.CreatedDate)
                                        .FirstOrDefault();
                                    
                                    // Determine the score to display: prioritize retake new score, then active old score
                                    var displayScore = retakeScore ?? activeScore;
                                    
                                    if (displayScore != null)
                                    {
                                        loResult.LOScore = displayScore.Score;
                                        loResult.LOPercentage = Math.Round((displayScore.Score / assessmentLO.Score) * 100, 2);
                                        loResult.LOPassed = loResult.LOPercentage >= 50;
                                        loResult.NeedsRetake = !loResult.LOPassed;
                                        
                                        // Set retake related information
                                        if (retakeScore != null)
                                        {
                                            loResult.IsRetake = true;
                                            loResult.RetakeDate = retakeScore.RetakeDate;
                                            var retakePercentage = Math.Round((retakeScore.Score / assessmentLO.Score) * 100, 2);
                                            
                                                            // Each LO under each assessment is judged separately for retake status
                // If the LO under this assessment passes retake, show "retake passed"; if fails, show "retake failed"
                                            var currentAssessmentRetakePassed = retakePercentage >= 50;
                                            loResult.RetakePassed = currentAssessmentRetakePassed;
                                            loResult.RetakeFailed = !currentAssessmentRetakePassed;
                                            
                                                            // Note: The overall LO Overview retake status will be calculated elsewhere
                // Only when all instances of the same LO across all assessments pass retake, the overall LO will show "retake passed"
                                        }
                                        else
                                        {
                                            loResult.IsRetake = false;
                                            loResult.RetakeDate = null;
                                            loResult.RetakePassed = false;
                                            loResult.RetakeFailed = false;
                                        }
                                        
                                        loResult.HasRetake = allScoresForLO.Any(s => s.IsRetake);
                                        
                                        // Add historical scores for this LO (other scores that are not the current display score)
                                        try
                                        {
                                            var historicalScores = allScoresForLO
                                                .Where(s => s.Id != displayScore.Id)
                                                .OrderByDescending(s => s.CreatedDate);
                                            
                                            if (historicalScores.Any())
                                            {
                                                loResult.HistoricalScores = historicalScores.Select(hs => new HistoricalScoreDTO
                                                {
                                                    Score = hs.Score,
                                                    MaxScore = assessmentLO.Score,
                                                    Date = hs.CreatedDate ?? DateTime.Now
                                                }).ToList();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            // Log warning but don't fail the entire operation
                                            Console.WriteLine($"Warning: Failed to get historical scores for LO: {ex.Message}");
                                        }
                                    }
                                    else
                                    {
                                        // No active scores found
                                        loResult.LOScore = 0;
                                        loResult.LOPercentage = 0;
                                        loResult.LOPassed = false;
                                        loResult.NeedsRetake = true;
                                        loResult.HasRetake = false;
                                        loResult.IsRetake = false;
                                        loResult.RetakeDate = null;
                                        loResult.RetakePassed = false;
                                        loResult.RetakeFailed = false;
                                    }
                                }
                                else
                                {
                                    loResult.LOScore = 0;
                                    loResult.LOPercentage = 0;
                                    loResult.LOPassed = false;
                                    loResult.NeedsRetake = true;
                                    loResult.HasRetake = false;
                                    loResult.IsRetake = false;
                                    loResult.RetakeDate = null;
                                    loResult.RetakePassed = false;
                                    loResult.RetakeFailed = false;
                                }

                                assessmentResult.LearningOutcomes.Add(loResult);
                            }

                            // Calculate assessment total score
                            assessmentResult.AssessmentScore = Math.Round(assessmentResult.LearningOutcomes.Sum(lo => lo.LOScore), 2);
                            assessmentResult.AssessmentPercentage = Math.Round((assessmentResult.AssessmentScore / assessmentResult.MaxAssessmentScore) * 100, 2);
                            assessmentResult.AssessmentPassed = assessmentResult.AssessmentPercentage >= 50;
                        }
                        else
                        {
                            // Student has no score for this assessment, create empty result
                            foreach (var assessmentLO in assessment.AssessmentLearningOutcomes)
                            {
                                var loResult = new LearningOutcomeResultDTO
                                {
                                    LearningOutcomeId = assessmentLO.LOId,
                                    LearningOutcomeName = assessmentLO.LearningOutcome.LOName,
                                    AssessmentLearningOutcomeId = assessmentLO.Id,
                                    MaxLOScore = assessmentLO.Score,
                                    LOScore = 0,
                                    LOPercentage = 0,
                                    LOPassed = false,
                                    NeedsRetake = true,
                                    HasRetake = false,
                                    IsRetake = false,
                                    HasFailed = true
                                };

                                // Add to failed assessments
                                loResult.FailedAssessments.Add(new FailedAssessmentDTO
                                {
                                    AssessmentId = assessment.AssessmentId,
                                    AssessmentName = assessment.AssessmentName,
                                    LOScore = 0,
                                    MaxLOScore = assessmentLO.Score,
                                    LOPercentage = 0
                                });

                                assessmentResult.LearningOutcomes.Add(loResult);
                            }

                            assessmentResult.AssessmentScore = 0;
                            assessmentResult.AssessmentPercentage = 0;
                            assessmentResult.AssessmentPassed = false;
                        }

                        studentResult.Assessments.Add(assessmentResult);
                    }

                    // Set failure status and failed assessments
                    foreach (var assessment in studentResult.Assessments)
                    {
                        foreach (var lo in assessment.LearningOutcomes)
                        {
                            if (!lo.LOPassed)
                            {
                                lo.HasFailed = true;
                                lo.FailedAssessments.Add(new FailedAssessmentDTO
                                {
                                    AssessmentId = assessment.AssessmentId,
                                    AssessmentName = assessment.AssessmentName,
                                    LOScore = lo.LOScore,
                                    MaxLOScore = lo.MaxLOScore,
                                    LOPercentage = lo.LOPercentage
                                });
                            }
                        }
                    }

                    // Calculate overall scores
                    studentResult.MaxTotalScore = Math.Round(studentResult.Assessments.Sum(a => a.MaxAssessmentScore), 2);
                    studentResult.TotalScore = Math.Round(studentResult.Assessments.Sum(a => a.AssessmentScore), 2);
                    studentResult.TotalPercentage = Math.Round((studentResult.TotalScore / studentResult.MaxTotalScore) * 100, 2);
                    studentResult.OverallPassed = studentResult.TotalPercentage >= 50;
                    studentResult.NeedsResit = !studentResult.OverallPassed;
                }
                else
                {
                    // Handle case where student has no assessment scores
                    // Create empty assessment results for all assessments
                    foreach (var assessment in assessmentsWithLOs)
                    {
                        var assessmentResult = new AssessmentResultDTO
                        {
                            AssessmentId = assessment.AssessmentId,
                            AssessmentName = assessment.AssessmentName,
                            MaxAssessmentScore = assessment.MaxAssessmentScore,
                            Weight = assessment.Weight,
                            AssessmentScore = 0,
                            AssessmentPercentage = 0,
                            AssessmentPassed = false,
                            LearningOutcomes = new List<LearningOutcomeResultDTO>()
                        };

                        foreach (var assessmentLO in assessment.AssessmentLearningOutcomes)
                        {
                            var loResult = new LearningOutcomeResultDTO
                            {
                                LearningOutcomeId = assessmentLO.LOId,
                                LearningOutcomeName = assessmentLO.LearningOutcome.LOName,
                                AssessmentLearningOutcomeId = assessmentLO.Id,
                                MaxLOScore = assessmentLO.Score,
                                LOScore = 0,
                                LOPercentage = 0,
                                LOPassed = false,
                                NeedsRetake = true,
                                HasRetake = false,
                                IsRetake = false,
                                HasFailed = true
                            };

                            // Add to failed assessments
                            loResult.FailedAssessments.Add(new FailedAssessmentDTO
                            {
                                AssessmentId = assessment.AssessmentId,
                                AssessmentName = assessment.AssessmentName,
                                LOScore = 0,
                                MaxLOScore = assessmentLO.Score,
                                LOPercentage = 0
                            });

                            assessmentResult.LearningOutcomes.Add(loResult);
                        }

                        studentResult.Assessments.Add(assessmentResult);
                    }

                    // Set overall scores for student with no scores
                    studentResult.MaxTotalScore = studentResult.Assessments.Sum(a => a.MaxAssessmentScore);
                    studentResult.TotalScore = 0;
                    studentResult.TotalPercentage = 0;
                    studentResult.OverallPassed = false;
                    studentResult.NeedsResit = true;
                }

                return studentResult;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to build student result from batch data: {ex.Message}");
            }
        }

        // Validate batch data integrity
        private bool ValidateBatchData(
            IEnumerable<AssessmentWithLOsDTO> assessmentsWithLOs,
            IEnumerable<StudentAssessmentScore> allStudentScores,
            IEnumerable<StudentLOScore> allLOScores)
        {
            try
            {
                // Check if assessments have learning outcomes
                foreach (var assessment in assessmentsWithLOs)
                {
                    if (!assessment.AssessmentLearningOutcomes.Any())
                    {
                        Console.WriteLine($"Warning: Assessment {assessment.AssessmentId} has no learning outcomes");
                        return false;
                    }
                }

                // Check if student scores are properly linked
                foreach (var studentScore in allStudentScores)
                {
                    if (string.IsNullOrEmpty(studentScore.StudentId))
                    {
                        Console.WriteLine($"Warning: Student score {studentScore.Id} has no student ID");
                        return false;
                    }
                }

                // Check if LO scores are properly linked
                foreach (var loScore in allLOScores)
                {
                    if (string.IsNullOrEmpty(loScore.AssessmentLearningOutcomeId))
                    {
                        Console.WriteLine($"Warning: LO score {loScore.Id} has no assessment learning outcome ID");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating batch data: {ex.Message}");
                return false;
            }
        }

        public async Task UpdateRetakeScoresAsync(RetakeRequestDTO retakeRequest)
        {
            try
            {
                // Validate that all scores are within valid range (0 to max score)
                foreach (var retakeScore in retakeRequest.RetakeScores)
                {
                    if (retakeScore.NewScore < 0)
                    {
                        throw new InvalidOperationException($"Score for assessment  cannot be negative");
                    }
                    if (retakeScore.NewScore > retakeScore.MaxScore)
                    {
                        throw new InvalidOperationException($"Score for assessment cannot exceed maximum score");
                    }
                }

                // Get all failed assessments for this LO
                var failedAssessments = await GetFailedAssessmentsForRetakeAsync(
                    retakeRequest.StudentId, 
                    retakeRequest.CourseOfferingId, 
                    retakeRequest.LearningOutcomeName);

                if (!failedAssessments.Any())
                {
                    throw new InvalidOperationException("No failed assessments found for this learning outcome");
                }

                // Deactivate old scores and create new retake scores
                foreach (var retakeScore in retakeRequest.RetakeScores)
                {
                    // Find the corresponding failed assessment
                    var failedAssessment = failedAssessments.FirstOrDefault(fa => fa.AssessmentId == retakeScore.AssessmentId);
                    if (failedAssessment == null)
                    {
                        throw new InvalidOperationException($"Assessment not found in failed assessments");
                    }

                    // Get the student assessment score record
                    var studentAssessmentScore = await _unitOfWork.studentScoreRepository
                        .GetStudentScoreByStudentAssessmentAsync(retakeRequest.StudentId, retakeScore.AssessmentId);
                    
                    if (studentAssessmentScore == null)
                    {
                        throw new InvalidOperationException($"Student assessment score not found");
                    }

                    // Get the original LO score record
                    var originalLOScore = await _unitOfWork.studentLOScoreRepository
                        .GetStudentLOScoresByStudentAssessmentScoreAsync(studentAssessmentScore.Id);
                    
                    var loScore = originalLOScore.FirstOrDefault(sls => 
                        sls.AssessmentLearningOutcome.LearningOutcome.LOName == retakeRequest.LearningOutcomeName);
                    
                    if (loScore == null)
                    {
                        throw new InvalidOperationException($"Learning outcome score not found");
                    }

                    // Deactivate old score
                    loScore.IsActive = false;
                    _unitOfWork.studentLOScoreRepository.Update(loScore);

                    // Create new retake score
                    var newRetakeScore = new StudentLOScore
                    {
                        Id = Guid.NewGuid().ToString(),
                        StudentAssessmentScoreId = studentAssessmentScore.Id,
                        AssessmentLearningOutcomeId = loScore.AssessmentLearningOutcomeId,
                        Score = retakeScore.NewScore,
                        IsActive = true,
                        IsRetake = true,
                        CreatedDate = DateTime.UtcNow,
                        RetakeDate = DateTime.UtcNow
                    };

                    await _unitOfWork.studentLOScoreRepository.AddAsync(newRetakeScore);
                }

                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to update retake scores");
            }
        }

        public async Task<List<FailedAssessmentForRetakeDTO>> GetFailedAssessmentsForRetakeAsync(string studentId, string courseOfferingId, string loName)
        {
            try
            {
                
                var result = new List<FailedAssessmentForRetakeDTO>();
                
                // Get all assessments for this course offering
                var assessments = await _unitOfWork.assessmentRepository.GetAssessmentsByCourseOfferingId(courseOfferingId);
                
                foreach (var assessment in assessments)
                {
                    // Get the LO for this assessment
                    var assessmentLOs = await _unitOfWork.assessmentRepository.GetLOListByAssessmentId(assessment.Id);
                    
                    var lo = assessmentLOs.FirstOrDefault(alo => 
                        alo.LearningOutcome.LOName == loName);
                    
                    if (lo != null)
                    {
                        // Get student's score for this assessment
                        var studentAssessmentScore = await _unitOfWork.studentScoreRepository
                            .GetStudentScoreByStudentAssessmentAsync(studentId, assessment.Id);
                        
                        if (studentAssessmentScore != null)
                        {
                            // Get LO score for this student
                            var studentLOScores = await _unitOfWork.studentLOScoreRepository
                                .GetStudentLOScoresByStudentAssessmentScoreAsync(studentAssessmentScore.Id);
                            
                            var loScore = studentLOScores.FirstOrDefault(sls => 
                                sls.AssessmentLearningOutcomeId == lo.Id);
                            
                            if (loScore != null && (loScore.Score / lo.Score) * 100 < 50)
                            {
                                result.Add(new FailedAssessmentForRetakeDTO
                                {
                                    AssessmentId = assessment.Id,
                                    AssessmentName = assessment.AssessmentName,
                                    LOScore = loScore.Score,
                                    MaxLOScore = lo.Score,
                                    LOPercentage = (loScore.Score / lo.Score) * 100
                                });
                            }
                        }
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get failed assessments for retake");
            }
        }
    }
}
