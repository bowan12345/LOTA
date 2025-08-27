using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateScoreTOAssessmentLOTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "AssessmentLearningOutcome",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e626f9bf-2899-452d-b01f-c2a8f1d63b49", "3847a896-3cf8-49d8-80b8-0caac10116d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e3062c68-c2b2-44bb-8448-3fff6dfa98ec", "88c06e2a-16da-43da-975b-3b56cdc5ef0d" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6420));

            migrationBuilder.UpdateData(
                table: "AssessmentLearningOutcome",
                keyColumn: "Id",
                keyValue: "ALO1001",
                column: "Score",
                value: 60m);

            migrationBuilder.UpdateData(
                table: "AssessmentLearningOutcome",
                keyColumn: "Id",
                keyValue: "ALO1002",
                column: "Score",
                value: 40m);

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6293));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6296));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6377));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6380));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6221));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6273));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6457), new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6455) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6474));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6333));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6335));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6336));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6338));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 19, 27, 14, 944, DateTimeKind.Local).AddTicks(6358));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
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
        }
    }
}
