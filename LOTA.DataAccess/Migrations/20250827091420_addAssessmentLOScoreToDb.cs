using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addAssessmentLOScoreToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentScore");

            migrationBuilder.CreateTable(
                name: "StudentAssessmentScore",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssessmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LOId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrimesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAssessmentScore_LearningOutcome_LOId",
                        column: x => x.LOId,
                        principalTable: "LearningOutcome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAssessmentScore_Trimester_TrimesterId",
                        column: x => x.TrimesterId,
                        principalTable: "Trimester",
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1fd7af2f-308f-492e-9cc8-0279a7ba799b", "cdc5eb16-9e85-4407-8875-23fdda7a2e1f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "45eb7125-4da8-4b52-8bc5-0a72288a154c", "7f110aad-1163-4230-a5ca-c5106a51f665" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(97));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(100));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(180));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(182));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(25));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(77));

            migrationBuilder.InsertData(
                table: "StudentAssessmentScore",
                columns: new[] { "Id", "AssessmentId", "CreatedDate", "IsActive", "IsRetake", "LOId", "RetakeDate", "StudentId", "TotalScore", "TrimesterId", "UpdatedDate" },
                values: new object[] { "SCORE-001", "ASSIGN-001", new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(274), true, false, "LO-001", null, "STUDENT-001", 100m, "Trimester-001", null });

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(257), new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(256) });

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(140));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(142));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(144));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(146));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(164));

            migrationBuilder.InsertData(
                table: "StudentLOScore",
                columns: new[] { "Id", "CreatedDate", "IsActive", "Score", "StudentAssessmentScoreId", "UpdatedDate" },
                values: new object[,]
                {
                    { "LOSCORE-001", new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(289), true, 50m, "SCORE-001", null },
                    { "LOSCORE-002", new DateTime(2025, 8, 27, 21, 14, 19, 484, DateTimeKind.Local).AddTicks(292), true, 50m, "SCORE-001", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessmentScore_AssessmentId",
                table: "StudentAssessmentScore",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessmentScore_LOId",
                table: "StudentAssessmentScore",
                column: "LOId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessmentScore_StudentId",
                table: "StudentAssessmentScore",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessmentScore_TrimesterId",
                table: "StudentAssessmentScore",
                column: "TrimesterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLOScore_StudentAssessmentScoreId",
                table: "StudentLOScore",
                column: "StudentAssessmentScoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentLOScore");

            migrationBuilder.DropTable(
                name: "StudentAssessmentScore");

            migrationBuilder.CreateTable(
                name: "StudentScore",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssessmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LOId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrimesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRetake = table.Column<bool>(type: "bit", nullable: true),
                    RetakeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
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
                        name: "FK_StudentScore_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e626f9bf-2899-452d-b01f-c2a8f1d63b49", "3847a896-3cf8-49d8-80b8-0caac10116d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e3062c68-c2b2-44bb-8448-3fff6dfa98ec", "88c06e2a-16da-43da-975b-3b56cdc5ef0d" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6420));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6293));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6296));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6377));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6380));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6221));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6273));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6457), new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6455) });

            migrationBuilder.InsertData(
                table: "StudentScore",
                columns: new[] { "Id", "AssessmentId", "CreatedDate", "IsRetake", "LOId", "RetakeDate", "Score", "Status", "StudentId", "TrimesterId", "UpdatedDate" },
                values: new object[] { "SCORE-001", "ASSIGN-001", new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6474), false, "LO-001", null, 80m, "Pass", "STUDENT-001", "Trimester-001", null });

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6333));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6335));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6336));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6338));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6358));

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
        }
    }
}
