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

        public async Task<List<LOResultDTO>> GetLOResultsByQualificationAsync()
        {
            try
            {
                // Get the latest trimester first
                var latestTrimester = await _unitOfWork.trimesterRepository.GetLatestTrimestersAsync();
                if (latestTrimester == null)
                {
                    throw new InvalidOperationException("No trimester information found");
                }

                var qualifications = await _unitOfWork.qualificationRepository.GetAllAsync();
                var result = new List<LOResultDTO>();

                foreach (var qualification in qualifications)
                {
                    var qualificationResult = new LOResultDTO
                    {
                        QualificationId = qualification.Id,
                        QualificationName = qualification.QualificationName,
                        CourseOfferings = new List<CourseOfferingResultDTO>()
                    };

                    // Get course offerings for this qualification in the latest trimester only
                    var courseOfferings = await _unitOfWork.trimesterCourseRepository.GetTrimesterCoursesByTrimesterAsync(latestTrimester.Id);
                    var filteredCourseOfferings = courseOfferings.Where(tc => tc.Course.QualificationId == qualification.Id);

                    foreach (var courseOffering in filteredCourseOfferings)
                    {
                        var courseOfferingResult = new CourseOfferingResultDTO
                        {
                            CourseOfferingId = courseOffering.Id,
                            CourseName = courseOffering.Course.CourseName,
                            CourseCode = courseOffering.Course.CourseCode,
                            TrimesterName = $"Trimester {courseOffering.Trimester.TrimesterNumber} {courseOffering.Trimester.AcademicYear}",
                            Students = new List<StudentResultDTO>()
                        };

                        // Get students enrolled in this course offering using existing repository method
                        var enrolledStudents = await _unitOfWork.studentCourseRepository.GetByCourseOfferingIdAsync(courseOffering.Id);

                        foreach (var studentCourse in enrolledStudents)
                        {
                            var studentResult = await GetStudentResultAsync(studentCourse.StudentId, courseOffering.Id);
                            if (studentResult != null)
                            {
                                courseOfferingResult.Students.Add(studentResult);
                            }
                        }

                        qualificationResult.CourseOfferings.Add(courseOfferingResult);
                    }

                    // Only add qualifications that have course offerings
                    if (qualificationResult.CourseOfferings.Any())
                    {
                        result.Add(qualificationResult);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get LO results: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TrimesterCourse>> GetLatestTrimesterCourseOfferingsAsync()
        {
            try
            {
                return await _unitOfWork.trimesterCourseRepository.GetLatestTrimesterCourseOfferingsAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get latest trimester course offerings: {ex.Message}");
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
                        throw new InvalidOperationException($"Qualification information not found for course {courseOffering.Course.CourseCode}. CourseId: {courseOffering.CourseId}, QualificationId: {courseOffering.Course.QualificationId}");
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

                foreach (var studentCourse in enrolledStudents)
                {
                    var studentResult = await GetStudentResultAsync(studentCourse.StudentId, courseOffering.Id);
                    if (studentResult != null)
                    {
                        courseOfferingResult.Students.Add(studentResult);
                    }
                }

                qualificationResult.CourseOfferings.Add(courseOfferingResult);
                return qualificationResult;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get LO results by course offering: {ex.Message}");
            }
        }

        public async Task<StudentResultDTO> GetStudentLOResultAsync(string studentId, string courseOfferingId)
        {
            try
            {
                return await GetStudentResultAsync(studentId, courseOfferingId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get student LO result: {ex.Message}");
            }
        }

        public async Task<bool> UpdateStudentLOScoreAsync(string studentId, string assessmentId, string learningOutcomeId, decimal score)
        {
            try
            {
                // First check if StudentAssessmentScore exists
                var studentAssessmentScore = await _unitOfWork.studentScoreRepository.GetStudentScoreByStudentAssessmentAsync(studentId, assessmentId);
                if (studentAssessmentScore == null)
                {
                    throw new InvalidOperationException("Student assessment score not found");
                }

                // Get existing LO score if it exists
                var studentLOScores = await _unitOfWork.studentLOScoreRepository.GetStudentLOScoresByStudentAssessmentScoreAsync(studentAssessmentScore.Id);
                var existingScore = studentLOScores.FirstOrDefault(sls => sls.AssessmentLearningOutcomeId == learningOutcomeId);

                if (existingScore != null)
                {
                    existingScore.Score = score;
                    existingScore.UpdatedDate = DateTime.UtcNow;
                    _unitOfWork.studentLOScoreRepository.Update(existingScore);
                }
                else
                {
                    // Create new score if doesn't exist
                    var newScore = new StudentLOScore
                    {
                        Id = Guid.NewGuid().ToString(),
                        StudentAssessmentScoreId = studentAssessmentScore.Id,
                        AssessmentLearningOutcomeId = learningOutcomeId,
                        Score = score,
                        IsActive = true,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _unitOfWork.studentLOScoreRepository.AddAsync(newScore);
                }

                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to update student LO score: {ex.Message}");
            }
        }

        public async Task<bool> UpdateRetakeScoresAsync(RetakeScoreDTO retakeScoreDTO)
        {
            try
            {
                foreach (var retakeScore in retakeScoreDTO.RetakeScores)
                {
                    // Get the assessment to find the learning outcome
                    var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(retakeScore.AssessmentId);
                    if (assessment == null) continue;
                    
                    // Find the learning outcome by name
                    var learningOutcome = assessment.AssessmentLearningOutcomes
                        .FirstOrDefault(alo => alo.LearningOutcome.LOName == retakeScoreDTO.LearningOutcomeName);
                    
                    if (learningOutcome == null) continue;
                    
                    // Get student assessment score for this assessment
                    var studentAssessmentScore = await _unitOfWork.studentScoreRepository
                        .GetStudentScoreByStudentAssessmentAsync(retakeScoreDTO.StudentId, retakeScore.AssessmentId);
                    
                    if (studentAssessmentScore == null) continue;
                    
                    // Get existing student LO scores for this assessment and learning outcome
                    var existingScores = await _unitOfWork.studentLOScoreRepository
                        .GetStudentLOScoresByStudentAssessmentScoreAsync(studentAssessmentScore.Id);
                    
                    var existingScore = existingScores.FirstOrDefault(s => s.AssessmentLearningOutcomeId == learningOutcome.Id);
                    
                    // Deactivate existing score if it exists
                    if (existingScore != null)
                    {
                        existingScore.IsActive = false;
                        _unitOfWork.studentLOScoreRepository.Update(existingScore);
                    }
                    
                    // Create new retake score
                    var newRetakeScore = new StudentLOScore
                    {
                        Id = Guid.NewGuid().ToString(),
                        StudentAssessmentScoreId = studentAssessmentScore.Id,
                        AssessmentLearningOutcomeId = learningOutcome.Id,
                        Score = retakeScore.NewScore,
                        IsActive = true,
                        CreatedDate = DateTime.UtcNow
                    };
                    
                    await _unitOfWork.studentLOScoreRepository.AddAsync(newRetakeScore);
                }
                
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to update retake scores: {ex.Message}");
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
                throw new InvalidOperationException($"Failed to get retake history: {ex.Message}");
            }
        }
        
        public async Task<List<object>> GetFailedAssessmentsForRetakeAsync(string studentId, string courseOfferingId, string loName)
        {
            try
            {
                var failedAssessments = new List<object>();
                
                // Get all assessments
                var allAssessments = await _unitOfWork.assessmentRepository.GetAllAsync();
                
                // Filter assessments that belong to the course offering
                var courseAssessments = allAssessments.Where(assessment => assessment.CourseOfferingId == courseOfferingId).ToList();
                
                foreach (var assessment in courseAssessments)
                {
                    // Find the learning outcome in this assessment
                    var assessmentLearningOutcome = assessment.AssessmentLearningOutcomes
                        .FirstOrDefault(assessmentLearningOutcome => assessmentLearningOutcome.LearningOutcome.LOName == loName);
                    
                    if (assessmentLearningOutcome == null)
                    {
                        continue;
                    }
                    
                    // Get student's score for this assessment and LO
                    var studentAssessmentScore = await _unitOfWork.studentScoreRepository
                        .GetStudentScoreByStudentAssessmentAsync(studentId, assessment.Id);
                    
                    if (studentAssessmentScore == null)
                    {
                        continue;
                    }
                    
                    // Get all student LO scores for this assessment
                    var studentLOScores = await _unitOfWork.studentLOScoreRepository
                        .GetStudentLOScoresByStudentAssessmentScoreAsync(studentAssessmentScore.Id);
                    
                    var studentLOScore = studentLOScores.FirstOrDefault(studentLOScore => studentLOScore.AssessmentLearningOutcomeId == assessmentLearningOutcome.Id);
                    
                    if (studentLOScore != null)
                    {
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
                
                return failedAssessments;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get failed assessments for retake: {ex.Message}");
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
                throw new InvalidOperationException($"Failed to get retake data by assessment IDs: {ex.Message}");
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
                            MaxLOScore = assessmentLO.Score
                        };

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
                                
                                // Check if this LO has retake history
                                var allScoresForLO = await _unitOfWork.studentLOScoreRepository
                                    .GetStudentLOScoresByStudentAssessmentScoreAsync(studentAssessmentScore.Id);
                                var loScores = allScoresForLO.Where(s => s.AssessmentLearningOutcomeId == assessmentLO.Id);
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
                throw new InvalidOperationException($"Failed to get student result: {ex.Message}");
            }
        }
    }
}
