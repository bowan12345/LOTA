using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateCourseNameShcema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CourseName",
                table: "Course",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fcd9f10c-1a09-4694-99de-44f80593e1cc", "e4ef04fb-9bbb-4746-88b2-1dd0afd5d483" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0870d2a5-2fed-444a-a0b9-ce1a16b1bdbb", "7a2caaa4-54a9-480f-8f4f-e268fe4899b8" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5345));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5216));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5220));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5303));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5305));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5143));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5197));

            migrationBuilder.UpdateData(
                table: "StudentAssessmentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5380), new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5379) });

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5410));

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5443));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5261));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5264));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5266));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5267));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 31, 13, 5, 31, 560, DateTimeKind.Local).AddTicks(5286));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CourseName",
                table: "Course",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ff37c2db-6b86-4bf5-931d-3f535813bcb1", "6b49ea99-41f0-482a-af09-e5c923acef11" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1d6c3e1e-68d9-4823-ad01-5b129d70f421", "ab1c8d55-2f8d-427f-aa75-d8989eb62ceb" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4154));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(3978));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(3981));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4074));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(3901));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(3955));

            migrationBuilder.UpdateData(
                table: "StudentAssessmentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4209));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4193), new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4191) });

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4225));

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4228));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4023));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4025));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4027));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4029));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4051));
        }
    }
}
