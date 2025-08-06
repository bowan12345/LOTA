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
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<AssignmentLearningOutcome> AssignmentLearningOutcome { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<LearningOutcome> LearningOutcome { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<StudentScore> StudentScore { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<StudentScore>()
                .HasOne(ss => ss.Assignment)
                .WithMany(a => a.StudentScores)
                .HasForeignKey(ss => ss.AssignmentId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<StudentScore>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentScores)
                .HasForeignKey(ss => ss.StudentId)// FK is ApplicationUser.StudentIdentifier
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<TutorCourse>()
                .HasOne(tc => tc.Tutor)
                .WithMany(u => u.TutorCourse)
                .HasPrincipalKey(u => u.Id)
                .HasForeignKey(tc => tc.TutorId) // FK is ApplicationUser.TutorIdentifier
                .OnDelete(DeleteBehavior.Restrict);

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
            // course
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = "COURSE-001",
                    CourseName = "Software Engineering",
                    CourseCode = "SE101",
                    Description = "Introduction to software development processes and methodologies.",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }, 
                new Course
                {
                    Id = "COURSE-002",
                    CourseName = "Software Testing",
                    CourseCode = "ST102",
                    Description = "Introduction to software Testing processes and methodologies.",
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

            // Learning Outcome
            modelBuilder.Entity<LearningOutcome>().HasData(
                new LearningOutcome
                {
                    Id = "LO-001",
                    LOName = "Requirement Analysis",
                    Description = "Understand and document software requirements effectively.",
                    MaxScore = 100,
                    Weight = 0.3M,
                    CourseId = "COURSE-001",
                    CreatedDate = DateTime.Now
                },
                new LearningOutcome
                {
                    Id = "LO-002",
                    LOName = "System Design",
                    Description = "Apply design principles to create robust software architectures.",
                    MaxScore = 100,
                    Weight = 0.4M,
                    CourseId = "COURSE-001",
                    CreatedDate = DateTime.Now
                }
            );

            // assignment
            modelBuilder.Entity<Assignment>().HasData(
                new Assignment
                {
                    Id = "ASSIGN-001",
                    AssignmentName = "Project Proposal",
                    TotalWeight = 30,
                    TotalScore = 100,
                    CourseId = "COURSE-001",
                    CreatedBy = "TUTOR-001",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            );

            // assignment and learning outcome
            modelBuilder.Entity<AssignmentLearningOutcome>().HasData(
                new AssignmentLearningOutcome
                {
                    Id= "ALO1001",
                    AssignmentId = "ASSIGN-001",
                    LOId = "LO-001"
                },
                new AssignmentLearningOutcome
                {
                    Id= "ALO1002",
                    AssignmentId = "ASSIGN-001",
                    LOId = "LO-002"
                }
            );

            // student course
            modelBuilder.Entity<StudentCourse>().HasData(
                new StudentCourse
                {
                    Id = "STCOURSE-001",
                    StudentId = "STUDENT-001",
                    CourseId = "COURSE-001",
                    IsActive = true,
                    RegistrationDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                }
            );

            // student score


            modelBuilder.Entity<StudentScore>().HasData(
                new StudentScore
                {
                    Id = "SCORE-001",
                    StudentId = "STUDENT-001",
                    AssignmentId = "ASSIGN-001",
                    LOId = "LO-001",
                    Score = 80,
                    Status = "Pass",
                    IsRetake = false,
                    CreatedDate = DateTime.Now
                }
            );

        }
    }
}