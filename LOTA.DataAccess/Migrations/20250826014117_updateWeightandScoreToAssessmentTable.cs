using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateWeightandScoreToAssessmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalWeight",
                table: "Assessment",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "TotalScore",
                table: "Assessment",
                newName: "Score");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "13f2490e-2308-41c0-8683-9bc017540f37", "953429b2-3131-4e93-8016-a09da66d6a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2e89c1d0-e003-486b-b1e4-3e446c068282", "4a0288d2-0f5b-4eb3-8583-1f33edf6e9d0" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9903));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9779));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9783));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9861));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9863));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9699));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9755));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9933), new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9932) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9954));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9818));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9820));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9822));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9823));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 13, 41, 14, 729, DateTimeKind.Local).AddTicks(9843));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Assessment",
                newName: "TotalWeight");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Assessment",
                newName: "TotalScore");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c4adf72c-0265-4828-90f8-dacf0fe1fa6c", "cf19a088-8efc-443b-97a4-88cf091a1d54" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4257e3b6-7d94-4ff0-a4ac-6095429bda63", "288cbc95-694f-48c1-a84f-1ac7c4f9bc99" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 131, DateTimeKind.Local).AddTicks(138));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 130, DateTimeKind.Local).AddTicks(9954));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 130, DateTimeKind.Local).AddTicks(9959));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 131, DateTimeKind.Local).AddTicks(76));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 131, DateTimeKind.Local).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 130, DateTimeKind.Local).AddTicks(9862));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 130, DateTimeKind.Local).AddTicks(9923));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 24, 22, 32, 22, 131, DateTimeKind.Local).AddTicks(188), new DateTime(2025, 8, 24, 22, 32, 22, 131, DateTimeKind.Local).AddTicks(186) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 131, DateTimeKind.Local).AddTicks(215));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 131, DateTimeKind.Local).AddTicks(13));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 131, DateTimeKind.Local).AddTicks(18));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 131, DateTimeKind.Local).AddTicks(20));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 131, DateTimeKind.Local).AddTicks(23));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 22, 32, 22, 131, DateTimeKind.Local).AddTicks(50));
        }
    }
}
