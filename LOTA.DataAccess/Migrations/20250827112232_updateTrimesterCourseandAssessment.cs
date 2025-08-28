using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateTrimesterCourseandAssessment : Migration
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
                name: "LearningOutcome",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LOName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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
                name: "Assessment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssessmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssessmentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseOfferingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TrimesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessment_AssessmentType_AssessmentTypeId",
                        column: x => x.AssessmentTypeId,
                        principalTable: "AssessmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessment_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assessment_TrimesterCourse_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalTable: "TrimesterCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assessment_Trimester_TrimesterId",
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

            migrationBuilder.CreateTable(
                name: "AssessmentLearningOutcome",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssessmentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LOId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentLearningOutcome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentLearningOutcome_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentLearningOutcome_LearningOutcome_LOId",
                        column: x => x.LOId,
                        principalTable: "LearningOutcome",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentAssessmentScore",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssessmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsRetake = table.Column<bool>(type: "bit", nullable: true),
                    RetakeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssessmentScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAssessmentScore_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAssessmentScore_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentLOScore",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentAssessmentScoreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLOScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentLOScore_StudentAssessmentScore_StudentAssessmentScoreId",
                        column: x => x.StudentAssessmentScoreId,
                        principalTable: "StudentAssessmentScore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StudentNo", "TutorNo", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "STUDENT-001", 0, "ea637cb3-a144-4857-96d0-a80ae3c9b594", "student1@lota.com", true, "Alice", true, "Brown", false, null, "STUDENT1@LOTA.COM", "STUDENT1@LOTA.COM", null, null, false, "887d4f53-da4f-408d-b6f4-a3281a71d6c0", "STUDENT-001", null, false, "student1@lota.com" },
                    { "TUTOR-001", 0, "bb00736b-a043-4e9d-98c1-cfeaa0e48708", "tutor1@lota.com", true, "John", true, "Smith", false, null, "TUTOR1@LOTA.COM", "TUTOR1@LOTA.COM", null, null, false, "2bca5028-5672-4df8-af1e-8ed026c824a0", null, "tutor1@lota.com", false, "tutor1@lota.com" }
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
                    { "Trimester-001", 2024, new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1878), true, 1, null },
                    { "Trimester-002", 2024, new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1880), true, 2, null },
                    { "Trimester-003", 2025, new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1882), true, 1, null },
                    { "Trimester-004", 2025, new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1884), true, 2, null }
                });

            migrationBuilder.InsertData(
                table: "Qualification",
                columns: new[] { "Id", "CreatedDate", "IsActive", "Level", "QualificationName", "QualificationTypeId", "UpdatedDate" },
                values: new object[,]
                {
                    { "Qualification-001", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1767), true, 7, "Bachelor of Information Technolog", "001", null },
                    { "Qualification-002", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1818), true, 5, "Diploma in IT Technical Support", "002", null }
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "Id", "CourseCode", "CourseName", "CreatedDate", "Description", "IsActive", "QualificationId", "UpdatedDate" },
                values: new object[,]
                {
                    { "COURSE-001", "SE101", "Software Engineering", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1840), "Introduction to software development processes and methodologies.", true, "Qualification-001", null },
                    { "COURSE-002", "ST102", "Software Testing", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1842), "Introduction to software Testing processes and methodologies.", true, "Qualification-002", null }
                });

            migrationBuilder.InsertData(
                table: "LearningOutcome",
                columns: new[] { "Id", "CourseId", "CreatedDate", "Description", "LOName", "UpdatedDate" },
                values: new object[,]
                {
                    { "LO-001", "COURSE-001", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1920), "Understand and document software requirements effectively.", "Requirement Analysis", null },
                    { "LO-002", "COURSE-001", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1922), "Apply design principles to create robust software architectures.", "System Design", null }
                });

            migrationBuilder.InsertData(
                table: "TrimesterCourse",
                columns: new[] { "Id", "CourseId", "CreatedDate", "IsActive", "RegistrationDate", "TrimesterId", "TutorId", "UpdatedDate" },
                values: new object[] { "TC001", "COURSE-001", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1901), true, null, "Trimester-001", "TUTOR-001", null });

            migrationBuilder.InsertData(
                table: "TutorCourse",
                columns: new[] { "Id", "CourseId", "TutorId" },
                values: new object[,]
                {
                    { "TCO1001", "COURSE-001", "TUTOR-001" },
                    { "TCO1002", "COURSE-002", "TUTOR-001" }
                });

            migrationBuilder.InsertData(
                table: "Assessment",
                columns: new[] { "Id", "AssessmentName", "AssessmentTypeId", "CourseId", "CourseOfferingId", "CreatedBy", "CreatedDate", "IsActive", "Score", "TrimesterId", "UpdatedDate", "Weight" },
                values: new object[] { "ASSIGN-001", "Project Proposal", "001", null, "TC001", "TUTOR-001", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1961), true, 100m, "Trimester-001", null, 30m });

            migrationBuilder.InsertData(
                table: "StudentCourse",
                columns: new[] { "Id", "CourseId", "CourseOfferingId", "CreatedDate", "IsActive", "RegistrationDate", "StudentId", "TrimesterId", "UpdatedDate" },
                values: new object[] { "STCOURSE-001", null, "TC001", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1999), true, new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1998), "STUDENT-001", "Trimester-001", null });

            migrationBuilder.InsertData(
                table: "AssessmentLearningOutcome",
                columns: new[] { "Id", "AssessmentId", "LOId", "Score" },
                values: new object[,]
                {
                    { "ALO1001", "ASSIGN-001", "LO-001", 60m },
                    { "ALO1002", "ASSIGN-001", "LO-002", 40m }
                });

            migrationBuilder.InsertData(
                table: "StudentAssessmentScore",
                columns: new[] { "Id", "AssessmentId", "CreatedDate", "IsActive", "IsRetake", "RetakeDate", "StudentId", "TotalScore", "UpdatedDate" },
                values: new object[] { "SCORE-001", "ASSIGN-001", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(2015), true, false, null, "STUDENT-001", 100m, null });

            migrationBuilder.InsertData(
                table: "StudentLOScore",
                columns: new[] { "Id", "CreatedDate", "IsActive", "Score", "StudentAssessmentScoreId", "UpdatedDate" },
                values: new object[,]
                {
                    { "LOSCORE-001", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(2028), true, 50m, "SCORE-001", null },
                    { "LOSCORE-002", new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(2030), true, 50m, "SCORE-001", null }
                });

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
                name: "IX_Assessment_AssessmentTypeId",
                table: "Assessment",
                column: "AssessmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_CourseId",
                table: "Assessment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_CourseOfferingId",
                table: "Assessment",
                column: "CourseOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_TrimesterId",
                table: "Assessment",
                column: "TrimesterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentLearningOutcome_AssessmentId",
                table: "AssessmentLearningOutcome",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentLearningOutcome_LOId",
                table: "AssessmentLearningOutcome",
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
                name: "IX_StudentAssessmentScore_AssessmentId",
                table: "StudentAssessmentScore",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessmentScore_StudentId",
                table: "StudentAssessmentScore",
                column: "StudentId");

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
                name: "IX_StudentLOScore_StudentAssessmentScoreId",
                table: "StudentLOScore",
                column: "StudentAssessmentScoreId");

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
                name: "AssessmentLearningOutcome");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "StudentLOScore");

            migrationBuilder.DropTable(
                name: "TutorCourse");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "LearningOutcome");

            migrationBuilder.DropTable(
                name: "StudentAssessmentScore");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "AssessmentType");

            migrationBuilder.DropTable(
                name: "TrimesterCourse");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Trimester");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropTable(
                name: "QualificationType");
        }
    }
}
