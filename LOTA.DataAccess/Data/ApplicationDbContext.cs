using LOTA.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LOTA.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Assessment> Assessment { get; set; }
        public DbSet<AssessmentType> AssessmentType { get; set; }
        public DbSet<AssessmentLearningOutcome> AssessmentLearningOutcome { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<LearningOutcome> LearningOutcome { get; set; }
        public DbSet<Qualification> Qualification { get; set; }
        public DbSet<QualificationType> QualificationType { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<StudentAssessmentScore> StudentAssessmentScore { get; set; }
        public DbSet<StudentLOScore> StudentLOScore { get; set; }
        public DbSet<TutorCourse> TutorCourse { get; set; }
        public DbSet<Trimester> Trimester { get; set; }
        public DbSet<TrimesterCourse> TrimesterCourse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.TrimesterCourse)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseOfferingId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<StudentAssessmentScore>()
                .HasOne(ss => ss.Assessment)
                .WithMany(a => a.StudentScores)
                .HasForeignKey(ss => ss.AssessmentId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<StudentAssessmentScore>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentScores)
                .HasForeignKey(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentAssessmentScore>()
                .HasMany(s => s.StudentLOScores)
                .WithOne(sc => sc.StudentAssessmentScore)
                .HasForeignKey(sc => sc.StudentAssessmentScoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TutorCourse>()
                .HasOne(tc => tc.Tutor)
                .WithMany(u => u.TutorCourse)
                .HasForeignKey(tc => tc.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TutorCourse>()
                .HasOne(tc => tc.Course)
                .WithMany(c => c.TutorCourses)
                .HasForeignKey(tc => tc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Qualification and Course relationship
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Qualification)
                .WithMany(q => q.Courses)
                .HasForeignKey(c => c.QualificationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrimesterCourse>()
               .HasOne(c => c.Tutor)
               .WithMany(q => q.TrimesterCourse)
               .HasForeignKey(c => c.TutorId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Qualification>()
                .HasOne(c => c.QualificationType)
                .WithMany(q => q.Qualifications)
                .HasForeignKey(c => c.QualificationTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assessment>()
                .HasMany(a => a.AssessmentLearningOutcomes)
                .WithOne(lo => lo.Assessment)
                .HasForeignKey(lo => lo.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<ApplicationUser>().HasData(
               new ApplicationUser
               {
                   Id = "TUTOR-001",
                   UserName = "tutor1@lota.com",
                   NormalizedUserName = "TUTOR1@LOTA.COM",
                   Email = "tutor1@lota.com",
                   NormalizedEmail = "TUTOR1@LOTA.COM",
                   EmailConfirmed = true,
                   FirstName = "John",
                   LastName = "Smith",
                   TutorNo = "tutor1@lota.com",
                   IsActive = true,
                   SecurityStamp = Guid.NewGuid().ToString()
               },
               new ApplicationUser
               {
                   Id = "STUDENT-001",
                   UserName = "student1@lota.com",
                   NormalizedUserName = "STUDENT1@LOTA.COM",
                   Email = "student1@lota.com",
                   NormalizedEmail = "STUDENT1@LOTA.COM",
                   EmailConfirmed = true,
                   FirstName = "Alice",
                   LastName = "Brown",
                   StudentNo= "STUDENT-001",
                   IsActive = true,
                   SecurityStamp = Guid.NewGuid().ToString()
               }
           );

            // qualificationType
            modelBuilder.Entity<QualificationType>().HasData(
                new QualificationType
                {
                    Id = "001",
                    QualificationTypeName = "Bachelor",
                },
                new QualificationType
                {
                    Id = "002",
                    QualificationTypeName = "Diploma",
                }
                ,
                new QualificationType
                {
                    Id = "003",
                    QualificationTypeName = "Certificate",
                }
                ,
                new QualificationType
                {
                    Id = "004",
                    QualificationTypeName = "Master",
                }
                ,
                new QualificationType
                {
                    Id = "005",
                    QualificationTypeName = "PhD",
                }
                ,
                new QualificationType
                {
                    Id = "006",
                    QualificationTypeName = "Graduate Diploma",
                }
                ,
                new QualificationType
                {
                    Id = "007",
                    QualificationTypeName = "Postgraduate Diploma",
                }
                ,
                new QualificationType
                {
                    Id = "008",
                    QualificationTypeName = "Postgraduate Certificate",
                }
            );
            // qualification
            modelBuilder.Entity<Qualification>().HasData(
                new Qualification
                {
                    Id = "Qualification-001",
                    QualificationName = "Bachelor of Information Technolog",
                    QualificationTypeId = "001",
                    Level=7,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Qualification
                {
                    Id = "Qualification-002",
                    QualificationName = "Diploma in IT Technical Support",
                    QualificationTypeId = "002",
                    Level = 5,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            );

            // course
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = "COURSE-001",
                    CourseName = "Software Engineering",
                    CourseCode = "SE101",
                    Description = "Introduction to software development processes and methodologies.",
                    QualificationId= "Qualification-001",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }, 
                new Course
                {
                    Id = "COURSE-002",
                    CourseName = "Software Testing",
                    CourseCode = "ST102",
                    Description = "Introduction to software Testing processes and methodologies.",
                    QualificationId = "Qualification-002",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            );

            // tutors and courses
            modelBuilder.Entity<TutorCourse>().HasData(
                new TutorCourse
                {
                    Id = "TCO1001",
                    TutorId = "TUTOR-001",
                    CourseId = "COURSE-001"
                },
                new TutorCourse
                {
                    Id = "TCO1002",
                    TutorId = "TUTOR-001",
                    CourseId = "COURSE-002"
                }
            );
            // trimester
            modelBuilder.Entity<Trimester>().HasData(
                new Trimester
                {
                    Id = "Trimester-001",
                    AcademicYear = 2024,
                    TrimesterNumber = 1,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Trimester
                {
                    Id = "Trimester-002",
                    AcademicYear = 2024,
                    TrimesterNumber = 2,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                 new Trimester
                 {
                     Id = "Trimester-003",
                     AcademicYear = 2025,
                     TrimesterNumber = 1,
                     IsActive = true,
                     CreatedDate = DateTime.Now
                 },
                new Trimester
                {
                    Id = "Trimester-004",
                    AcademicYear = 2025,
                    TrimesterNumber = 2,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            );

            // trimester and courses
            modelBuilder.Entity<TrimesterCourse>().HasData(
                new TrimesterCourse
                {
                    Id = "TC001",
                    TutorId = "TUTOR-001",
                    CourseId = "COURSE-001",
                    TrimesterId = "Trimester-001",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            );


            // Learning Outcome
            modelBuilder.Entity<LearningOutcome>().HasData(
                new LearningOutcome
                {
                    Id = "LO-001",
                    LOName = "Requirement Analysis",
                    Description = "Understand and document software requirements effectively.",
                    CourseId = "COURSE-001",
                    CreatedDate = DateTime.Now
                },
                new LearningOutcome
                {
                    Id = "LO-002",
                    LOName = "System Design",
                    Description = "Apply design principles to create robust software architectures.",
                    CourseId = "COURSE-001",
                    CreatedDate = DateTime.Now
                }
            );
            // assignmenttype
            modelBuilder.Entity<AssessmentType>().HasData(
                new AssessmentType
                {
                    Id = "001",
                    AssessmentTypeName = "Assignment",
                },
                new AssessmentType
                {
                    Id = "002",
                    AssessmentTypeName = "Exam",
                },
                new AssessmentType
                {
                    Id = "003",
                    AssessmentTypeName = "Test",
                },
                new AssessmentType
                {
                    Id = "004",
                    AssessmentTypeName = "Project",
                },
                new AssessmentType
                {
                    Id = "005",
                    AssessmentTypeName = "Presentation",
                }
            );

            // assignment
            modelBuilder.Entity<Assessment>().HasData(
                new Assessment
                {
                    Id = "ASSIGN-001",
                    AssessmentName = "Project Proposal",
                    AssessmentTypeId = "001",
                    Weight = 30,
                    Score = 100,
                    CourseOfferingId = "TC001",
                    TrimesterId = "Trimester-001",
                    CreatedBy = "TUTOR-001",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            );

            // assignment and learning outcome
            modelBuilder.Entity<AssessmentLearningOutcome>().HasData(
                new AssessmentLearningOutcome
                {
                    Id= "ALO1001",
                    AssessmentId = "ASSIGN-001",
                    LOId = "LO-001",
                    Score = 60
                },
                new AssessmentLearningOutcome
                {
                    Id= "ALO1002",
                    AssessmentId = "ASSIGN-001",
                    LOId = "LO-002",
                    Score = 40
                }
            );

          

            // student course
            modelBuilder.Entity<StudentCourse>().HasData(
                new StudentCourse
                {
                    Id = "STCOURSE-001",
                    StudentId = "STUDENT-001",
                    CourseOfferingId = "TC001",
                    TrimesterId="Trimester-001",
                    IsActive = true,
                    RegistrationDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                }
            );

            // student assessment score
            modelBuilder.Entity<StudentAssessmentScore>().HasData(
                new StudentAssessmentScore
                {
                    Id = "SCORE-001",
                    StudentId = "STUDENT-001",
                    AssessmentId = "ASSIGN-001",
                    LOId = "LO-001",
                    TrimesterId = "Trimester-001",
                    TotalScore = 100,
                    IsActive = true,
                    IsRetake = false,
                    CreatedDate = DateTime.Now
                }
            );

            // student LO score
            modelBuilder.Entity<StudentLOScore>().HasData(
                new StudentLOScore
                {
                    Id = "LOSCORE-001",
                    StudentAssessmentScoreId = "SCORE-001",
                    Score = 50,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                 new StudentLOScore
                 {
                     Id = "LOSCORE-002",
                     StudentAssessmentScoreId = "SCORE-001",
                     Score = 50,
                     IsActive = true,
                     CreatedDate = DateTime.Now
                 }
            );

        }
    }
}