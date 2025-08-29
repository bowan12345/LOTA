using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateSeedsToLOTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b4dbe439-5aa9-4844-ac36-0dfa0e5dbedc", "37ba1a73-eb85-48b1-8280-bc96c77c34b2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5b472f72-0abd-4a55-aa92-8e5e711eaf5c", "6d4153c1-e0e9-4e11-a32b-f7d1494d9124" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7593));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7413));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7417));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                columns: new[] { "CreatedDate", "LOName" },
                values: new object[] { new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7542), "LO1" });

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                columns: new[] { "CreatedDate", "LOName" },
                values: new object[] { new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7546), "LO2" });

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7340));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7391));

            migrationBuilder.UpdateData(
                table: "StudentAssessmentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7648));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7631), new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7662));

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7665));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7495));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7498));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7501));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7503));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7523));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "92581e60-a57b-46a5-99d6-562b7946164e", "de0485fc-1716-4db7-9ec5-776c00361b61" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "973d45dd-4578-46be-811b-c5efc0ebb603", "ae88cca8-3499-4e90-baca-21c533c76626" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8074));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                columns: new[] { "CreatedDate", "LOName" },
                values: new object[] { new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8159), "Requirement Analysis" });

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                columns: new[] { "CreatedDate", "LOName" },
                values: new object[] { new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8161), "System Design" });

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(7995));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8048));

            migrationBuilder.UpdateData(
                table: "StudentAssessmentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8247));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8230), new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8229) });

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8263));

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8265));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8113));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8115));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8117));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8118));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8139));
        }
    }
}
