using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addMustChangPasswordToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MustChangePassword",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "MustChangePassword", "SecurityStamp" },
                values: new object[] { "14cb8d84-f569-4f49-b750-5c76f0d8bc80", false, "581f0c3d-7f44-4fcc-9e69-6a2cab891b78" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "MustChangePassword", "SecurityStamp" },
                values: new object[] { "98760801-8aa4-4886-a42f-e8750205da1e", false, "65b576bd-b26a-4511-b0d6-29951cd26278" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2983));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2825));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2928));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2930));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2734));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2801));

            migrationBuilder.UpdateData(
                table: "StudentAssessmentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(3127));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(3107), new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(3105) });

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(3150));

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(3153));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2873));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2875));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2878));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2880));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 9, 10, 12, 47, 6, 878, DateTimeKind.Local).AddTicks(2903));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MustChangePassword",
                table: "AspNetUsers");

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
    }
}
