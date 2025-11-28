using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixSnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StudentAssessmentScore",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9a795753-9d7e-4252-b0ae-86f37e90d9ae", "f88718bb-4d08-4f64-a2b8-bcc3b25cda81" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8d09c099-8a1f-4d76-b0a2-48a538b33cd0", "b3cddfb5-ffda-45b5-bea8-69f7585e960e" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(5079));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(4907));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(4910));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(5023));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(5026));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(4808));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(4881));

            migrationBuilder.UpdateData(
                table: "StudentAssessmentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                columns: new[] { "CreatedDate", "Status" },
                values: new object[] { new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(5146), null });

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(5126), new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(5124) });

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(5166));

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(5169));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(4937));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(4940));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(4942));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(4946));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 10, 47, 40, 149, DateTimeKind.Local).AddTicks(4970));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "StudentAssessmentScore");

            

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "14cb8d84-f569-4f49-b750-5c76f0d8bc80", "581f0c3d-7f44-4fcc-9e69-6a2cab891b78" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "98760801-8aa4-4886-a42f-e8750205da1e", "65b576bd-b26a-4511-b0d6-29951cd26278" });

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
    }
}
