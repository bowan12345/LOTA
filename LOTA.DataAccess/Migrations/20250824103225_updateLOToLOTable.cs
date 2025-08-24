using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateLOToLOTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxScore",
                table: "LearningOutcome");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "LearningOutcome");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MaxScore",
                table: "LearningOutcome",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "LearningOutcome",
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
                values: new object[] { "c3532576-9f52-45fd-be97-c94d08ba4a9b", "2231bdac-14de-4196-adc8-3ebc421e0448" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "dd7b29a6-43f6-48ce-803b-408fe7856924", "19b6ca7c-6c37-4af2-97a8-ce5798fc4824" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3753));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3756));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                columns: new[] { "CreatedDate", "MaxScore", "Weight" },
                values: new object[] { new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3829), 100m, 0.3m });

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                columns: new[] { "CreatedDate", "MaxScore", "Weight" },
                values: new object[] { new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3832), 100m, 0.4m });

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3735));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3901), new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3899) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3917));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3789));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3791));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3792));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3794));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3810));
        }
    }
}
