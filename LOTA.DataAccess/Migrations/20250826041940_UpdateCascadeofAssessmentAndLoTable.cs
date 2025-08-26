using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascadeofAssessmentAndLoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentLearningOutcome_Assessment_AssessmentId",
                table: "AssessmentLearningOutcome");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3b0cd8a3-8336-410b-894a-a89501dbd754", "c4eeddf4-227f-41b0-80b8-136a9b2e09fd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d3686ab5-1544-4973-90ea-47f637ee45d6", "d20a5c45-e534-4f1d-ada6-946b9c18acc0" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4757));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4588));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4591));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4717));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4719));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4516));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4566));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4786), new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4785) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4804));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4631));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4634));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4635));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4637));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 16, 19, 40, 100, DateTimeKind.Local).AddTicks(4655));

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentLearningOutcome_Assessment_AssessmentId",
                table: "AssessmentLearningOutcome",
                column: "AssessmentId",
                principalTable: "Assessment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentLearningOutcome_Assessment_AssessmentId",
                table: "AssessmentLearningOutcome");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b876e44e-e278-4126-b852-5c9349b8b349", "279bee1c-baa1-4f07-b1e7-30a9267addfb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b3928458-b1a5-4786-b148-f3442874bcee", "3ca160c1-cb9e-438e-90ce-3d0f2bbc9110" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7585));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7421));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7425));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7536));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7539));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7347));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7620), new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7617) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7638));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7469));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7470));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7472));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 23, 59, 980, DateTimeKind.Local).AddTicks(7518));

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentLearningOutcome_Assessment_AssessmentId",
                table: "AssessmentLearningOutcome",
                column: "AssessmentId",
                principalTable: "Assessment",
                principalColumn: "Id");
        }
    }
}
