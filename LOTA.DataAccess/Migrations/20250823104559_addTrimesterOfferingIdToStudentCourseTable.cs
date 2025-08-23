using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addTrimesterOfferingIdToStudentCourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TutorNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StudentNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssessmentTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualificationType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QualificationTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trimester",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcademicYear = table.Column<int>(type: "int", nullable: false),
                    TrimesterNumber = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trimester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QualificationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    QualificationTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualification_QualificationType_QualificationTypeId",
                        column: x => x.QualificationTypeId,
                        principalTable: "QualificationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QualificationId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_Qualification_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssessmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssessmentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    TotalScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TrimesterId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignment_AssessmentType_AssessmentTypeId",
                        column: x => x.AssessmentTypeId,
                        principalTable: "AssessmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignment_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assignment_Trimester_TrimesterId",
                        column: x => x.TrimesterId,
                        principalTable: "Trimester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningOutcome",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LOName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaxScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningOutcome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningOutcome_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrimesterCourse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrimesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TutorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrimesterCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrimesterCourse_AspNetUsers_TutorId",
                        column: x => x.TutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrimesterCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrimesterCourse_Trimester_TrimesterId",
                        column: x => x.TrimesterId,
                        principalTable: "Trimester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorCourse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TutorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorCourse_AspNetUsers_TutorId",
                        column: x => x.TutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TutorCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentLearningOutcome",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssessmentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LOId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentLearningOutcome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentLearningOutcome_Assignment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assignment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignmentLearningOutcome_LearningOutcome_LOId",
                        column: x => x.LOId,
                        principalTable: "LearningOutcome",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentScore",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssessmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LOId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrimesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsRetake = table.Column<bool>(type: "bit", nullable: true),
                    RetakeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentScore_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentScore_Assignment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentScore_LearningOutcome_LOId",
                        column: x => x.LOId,
                        principalTable: "LearningOutcome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentScore_Trimester_TrimesterId",
                        column: x => x.TrimesterId,
                        principalTable: "Trimester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseOfferingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrimesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourse_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentCourse_TrimesterCourse_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalTable: "TrimesterCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Trimester_TrimesterId",
                        column: x => x.TrimesterId,
                        principalTable: "Trimester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StudentNo", "TutorNo", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "STUDENT-001", 0, "ada0e837-fa46-45f1-b70d-14c229e27b14", "student1@lota.com", true, "Alice", true, "Brown", false, null, "STUDENT1@LOTA.COM", "STUDENT1@LOTA.COM", null, null, false, "bf9d2d8f-2b2a-4668-970f-b56c82d5ec9c", "STUDENT-001", null, false, "student1@lota.com" },
                    { "TUTOR-001", 0, "767fa71f-0416-48f3-8b30-f8325899eb06", "tutor1@lota.com", true, "John", true, "Smith", false, null, "TUTOR1@LOTA.COM", "TUTOR1@LOTA.COM", null, null, false, "6e6c3444-eb76-4526-b51a-496c4736dc99", null, "tutor1@lota.com", false, "tutor1@lota.com" }
                });

            migrationBuilder.InsertData(
                table: "AssessmentType",
                columns: new[] { "Id", "AssessmentTypeName" },
                values: new object[,]
                {
                    { "001", "Assignment" },
                    { "002", "Exam" },
                    { "003", "Test" },
                    { "004", "Project" },
                    { "005", "Presentation" }
                });

            migrationBuilder.InsertData(
                table: "QualificationType",
                columns: new[] { "Id", "QualificationTypeName" },
                values: new object[,]
                {
                    { "001", "Bachelor" },
                    { "002", "Diploma" },
                    { "003", "Certificate" },
                    { "004", "Master" },
                    { "005", "PhD" },
                    { "006", "Graduate Diploma" },
                    { "007", "Postgraduate Diploma" },
                    { "008", "Postgraduate Certificate" }
                });

            migrationBuilder.InsertData(
                table: "Trimester",
                columns: new[] { "Id", "AcademicYear", "CreatedDate", "IsActive", "TrimesterNumber", "UpdatedDate" },
                values: new object[,]
                {
                    { "Trimester-001", 2024, new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8651), true, 1, null },
                    { "Trimester-002", 2024, new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8653), true, 2, null },
                    { "Trimester-003", 2025, new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8655), true, 1, null },
                    { "Trimester-004", 2025, new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8657), true, 2, null }
                });

            migrationBuilder.InsertData(
                table: "Qualification",
                columns: new[] { "Id", "CreatedDate", "IsActive", "Level", "QualificationName", "QualificationTypeId", "UpdatedDate" },
                values: new object[,]
                {
                    { "Qualification-001", new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8530), true, 7, "Bachelor of Information Technolog", "001", null },
                    { "Qualification-002", new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8582), true, 5, "Diploma in IT Technical Support", "002", null }
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "Id", "CourseCode", "CourseName", "CreatedDate", "Description", "IsActive", "QualificationId", "UpdatedDate" },
                values: new object[,]
                {
                    { "COURSE-001", "SE101", "Software Engineering", new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8603), "Introduction to software development processes and methodologies.", true, "Qualification-001", null },
                    { "COURSE-002", "ST102", "Software Testing", new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8606), "Introduction to software Testing processes and methodologies.", true, "Qualification-002", null }
                });

            migrationBuilder.InsertData(
                table: "Assignment",
                columns: new[] { "Id", "AssessmentName", "AssessmentTypeId", "CourseId", "CreatedBy", "CreatedDate", "IsActive", "TotalScore", "TotalWeight", "TrimesterId", "UpdatedDate" },
                values: new object[] { "ASSIGN-001", "Project Proposal", "001", "COURSE-001", "TUTOR-001", new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8747), true, 100m, 30m, "Trimester-001", null });

            migrationBuilder.InsertData(
                table: "LearningOutcome",
                columns: new[] { "Id", "CourseId", "CreatedDate", "Description", "LOName", "MaxScore", "UpdatedDate", "Weight" },
                values: new object[,]
                {
                    { "LO-001", "COURSE-001", new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8702), "Understand and document software requirements effectively.", "Requirement Analysis", 100m, null, 0.3m },
                    { "LO-002", "COURSE-001", new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8705), "Apply design principles to create robust software architectures.", "System Design", 100m, null, 0.4m }
                });

            migrationBuilder.InsertData(
                table: "TrimesterCourse",
                columns: new[] { "Id", "CourseId", "CreatedDate", "IsActive", "RegistrationDate", "TrimesterId", "TutorId", "UpdatedDate" },
                values: new object[] { "TC001", "COURSE-001", new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8678), true, null, "Trimester-001", "TUTOR-001", null });

            migrationBuilder.InsertData(
                table: "TutorCourse",
                columns: new[] { "Id", "CourseId", "TutorId" },
                values: new object[,]
                {
                    { "TCO1001", "COURSE-001", "TUTOR-001" },
                    { "TCO1002", "COURSE-002", "TUTOR-001" }
                });

            migrationBuilder.InsertData(
                table: "AssignmentLearningOutcome",
                columns: new[] { "Id", "AssessmentId", "LOId" },
                values: new object[,]
                {
                    { "ALO1001", "ASSIGN-001", "LO-001" },
                    { "ALO1002", "ASSIGN-001", "LO-002" }
                });

            migrationBuilder.InsertData(
                table: "StudentCourse",
                columns: new[] { "Id", "CourseId", "CourseOfferingId", "CreatedDate", "IsActive", "RegistrationDate", "StudentId", "TrimesterId", "UpdatedDate" },
                values: new object[] { "STCOURSE-001", null, "TC001", new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8782), true, new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8781), "STUDENT-001", "Trimester-001", null });

            migrationBuilder.InsertData(
                table: "StudentScore",
                columns: new[] { "Id", "AssessmentId", "CreatedDate", "IsRetake", "LOId", "RetakeDate", "Score", "Status", "StudentId", "TrimesterId", "UpdatedDate" },
                values: new object[] { "SCORE-001", "ASSIGN-001", new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8801), false, "LO-001", null, 80m, "Pass", "STUDENT-001", "Trimester-001", null });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_AssessmentTypeId",
                table: "Assignment",
                column: "AssessmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_CourseId",
                table: "Assignment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_TrimesterId",
                table: "Assignment",
                column: "TrimesterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentLearningOutcome_AssessmentId",
                table: "AssignmentLearningOutcome",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentLearningOutcome_LOId",
                table: "AssignmentLearningOutcome",
                column: "LOId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_QualificationId",
                table: "Course",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningOutcome_CourseId",
                table: "LearningOutcome",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_QualificationTypeId",
                table: "Qualification",
                column: "QualificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseId",
                table: "StudentCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseOfferingId",
                table: "StudentCourse",
                column: "CourseOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_StudentId",
                table: "StudentCourse",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_TrimesterId",
                table: "StudentCourse",
                column: "TrimesterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentScore_AssessmentId",
                table: "StudentScore",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentScore_LOId",
                table: "StudentScore",
                column: "LOId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentScore_StudentId",
                table: "StudentScore",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentScore_TrimesterId",
                table: "StudentScore",
                column: "TrimesterId");

            migrationBuilder.CreateIndex(
                name: "IX_TrimesterCourse_CourseId",
                table: "TrimesterCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrimesterCourse_TrimesterId",
                table: "TrimesterCourse",
                column: "TrimesterId");

            migrationBuilder.CreateIndex(
                name: "IX_TrimesterCourse_TutorId",
                table: "TrimesterCourse",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorCourse_CourseId",
                table: "TutorCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorCourse_TutorId",
                table: "TutorCourse",
                column: "TutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssignmentLearningOutcome");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "StudentScore");

            migrationBuilder.DropTable(
                name: "TutorCourse");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TrimesterCourse");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "LearningOutcome");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AssessmentType");

            migrationBuilder.DropTable(
                name: "Trimester");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropTable(
                name: "QualificationType");
        }
    }
}
