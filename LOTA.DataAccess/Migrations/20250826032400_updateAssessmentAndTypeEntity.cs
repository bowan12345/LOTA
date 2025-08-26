using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateAssessmentAndTypeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_AssessmentType_AssessmentTypeId",
                table: "Assessment");

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
                name: "FK_Assessment_AssessmentType_AssessmentTypeId",
                table: "Assessment",
                column: "AssessmentTypeId",
                principalTable: "AssessmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_AssessmentType_AssessmentTypeId",
                table: "Assessment");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Assessment_AssessmentType_AssessmentTypeId",
                table: "Assessment",
                column: "AssessmentTypeId",
                principalTable: "AssessmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
