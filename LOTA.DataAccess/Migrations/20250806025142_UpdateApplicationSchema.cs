using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "TutorNo" },
                values: new object[] { "d90a5709-01d1-48e6-81fc-244182e185ec", "5701eeb3-0e70-4976-bee3-fc2d64acc811", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7943856f-25f7-4891-beae-b97e7d760244", "c05af296-da86-4254-bf85-baa198390b58" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 51, 41, 956, DateTimeKind.Local).AddTicks(8656));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 51, 41, 956, DateTimeKind.Local).AddTicks(8556));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 51, 41, 956, DateTimeKind.Local).AddTicks(8603));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 51, 41, 956, DateTimeKind.Local).AddTicks(8638));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 51, 41, 956, DateTimeKind.Local).AddTicks(8641));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 6, 14, 51, 41, 956, DateTimeKind.Local).AddTicks(8684), new DateTime(2025, 8, 6, 14, 51, 41, 956, DateTimeKind.Local).AddTicks(8683) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 51, 41, 956, DateTimeKind.Local).AddTicks(8699));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "TutorNo" },
                values: new object[] { "704456d4-132a-40c7-80ed-c47938143816", "c78d5bf3-3271-4734-9946-14f0a127323d", "tutor1@lota.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7695b517-e8af-4285-b423-29aa39982ea9", "cfb7713c-70f8-4dc5-bb68-9f95ab3cc2a4" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 45, 8, 12, DateTimeKind.Local).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 45, 8, 12, DateTimeKind.Local).AddTicks(8810));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 45, 8, 12, DateTimeKind.Local).AddTicks(8854));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 45, 8, 12, DateTimeKind.Local).AddTicks(8892));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 45, 8, 12, DateTimeKind.Local).AddTicks(8896));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 6, 14, 45, 8, 12, DateTimeKind.Local).AddTicks(8941), new DateTime(2025, 8, 6, 14, 45, 8, 12, DateTimeKind.Local).AddTicks(8940) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 6, 14, 45, 8, 12, DateTimeKind.Local).AddTicks(8955));
        }
    }
}
