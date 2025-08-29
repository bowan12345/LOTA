using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateFieldsToLOscoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRetake",
                table: "StudentAssessmentScore");

            migrationBuilder.DropColumn(
                name: "RetakeDate",
                table: "StudentAssessmentScore");

            migrationBuilder.AddColumn<bool>(
                name: "IsRetake",
                table: "StudentLOScore",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RetakeDate",
                table: "StudentLOScore",
                type: "datetime2",
                nullable: true);

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
                columns: new[] { "CreatedDate", "IsRetake", "RetakeDate" },
                values: new object[] { new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4225), false, null });

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-002",
                columns: new[] { "CreatedDate", "IsRetake", "RetakeDate" },
                values: new object[] { new DateTime(2025, 8, 29, 13, 3, 39, 947, DateTimeKind.Local).AddTicks(4228), false, null });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRetake",
                table: "StudentLOScore");

            migrationBuilder.DropColumn(
                name: "RetakeDate",
                table: "StudentLOScore");

            migrationBuilder.AddColumn<bool>(
                name: "IsRetake",
                table: "StudentAssessmentScore",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RetakeDate",
                table: "StudentAssessmentScore",
                type: "datetime2",
                nullable: true);

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
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7542));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7546));

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
                columns: new[] { "CreatedDate", "IsRetake", "RetakeDate" },
                values: new object[] { new DateTime(2025, 8, 29, 12, 27, 10, 774, DateTimeKind.Local).AddTicks(7648), false, null });

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
    }
}
