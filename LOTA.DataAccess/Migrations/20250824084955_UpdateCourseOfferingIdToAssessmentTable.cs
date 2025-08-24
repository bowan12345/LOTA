using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseOfferingIdToAssessmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseOfferingId",
                table: "Assignment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d57e8e6b-b801-4df4-afd6-ed94f10289d3", "c58e0d88-8c34-49d1-82f9-5ac86811f2fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b87089ef-8e51-43a0-b5a9-16b5e46480f9", "d216d9ca-492d-485f-97f9-048e80029b32" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                columns: new[] { "CourseId", "CourseOfferingId", "CreatedDate" },
                values: new object[] { null, "TC001", new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(783) });

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(645));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(649));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(738));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(741));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(556));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(620));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(817), new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(815) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(843));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(691));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(694));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(696));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(698));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(717));

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_CourseOfferingId",
                table: "Assignment",
                column: "CourseOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_TrimesterCourse_CourseOfferingId",
                table: "Assignment",
                column: "CourseOfferingId",
                principalTable: "TrimesterCourse",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_TrimesterCourse_CourseOfferingId",
                table: "Assignment");

            migrationBuilder.DropIndex(
                name: "IX_Assignment_CourseOfferingId",
                table: "Assignment");

            migrationBuilder.DropColumn(
                name: "CourseOfferingId",
                table: "Assignment");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ada0e837-fa46-45f1-b70d-14c229e27b14", "bf9d2d8f-2b2a-4668-970f-b56c82d5ec9c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "767fa71f-0416-48f3-8b30-f8325899eb06", "6e6c3444-eb76-4526-b51a-496c4736dc99" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                columns: new[] { "CourseId", "CreatedDate" },
                values: new object[] { "COURSE-001", new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8747) });

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8603));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8606));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8702));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8705));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8530));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8582));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8782), new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8781) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8801));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8651));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8653));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8655));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8657));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 22, 45, 59, 224, DateTimeKind.Local).AddTicks(8678));
        }
    }
}
