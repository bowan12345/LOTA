using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateTrimesterFiledsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TrimesterNumber",
                table: "Trimester",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AcademicYear",
                table: "Trimester",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ab21c575-3297-4dc6-ad8d-9d0bb85780d2", "46c581c5-e5c5-429b-8176-ae6d8566ada5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2a014253-5033-4d30-a165-3adcfac06a14", "3b5ef03a-ae7c-4665-9d24-1a9de6cc1330" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7920));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7853));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7857));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7897));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7901));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7784));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7828));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(8015), new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(8014) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(8033));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                columns: new[] { "AcademicYear", "CreatedDate", "TrimesterNumber" },
                values: new object[] { 2024, new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7982), 1 });

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                columns: new[] { "AcademicYear", "CreatedDate", "TrimesterNumber" },
                values: new object[] { 2024, new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7984), 2 });

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                columns: new[] { "AcademicYear", "CreatedDate", "TrimesterNumber" },
                values: new object[] { 2025, new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7986), 1 });

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                columns: new[] { "AcademicYear", "CreatedDate", "TrimesterNumber" },
                values: new object[] { 2025, new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7988), 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrimesterNumber",
                table: "Trimester",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AcademicYear",
                table: "Trimester",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4e098e80-e98b-4558-b34e-59690b9ef982", "0ab250b8-332e-4bce-894c-920bda20c6dd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6c76449c-0e8b-4fac-90e7-3be898c99ccc", "f582a589-915e-41e3-8184-b2fbd7412578" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9853));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9790));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9793));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9832));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9835));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9721));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9770));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9907), new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9906) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9922));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                columns: new[] { "AcademicYear", "CreatedDate", "TrimesterNumber" },
                values: new object[] { "2024", new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9884), "1" });

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                columns: new[] { "AcademicYear", "CreatedDate", "TrimesterNumber" },
                values: new object[] { "2024", new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9886), "2" });

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                columns: new[] { "AcademicYear", "CreatedDate", "TrimesterNumber" },
                values: new object[] { "2025", new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9888), "1" });

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                columns: new[] { "AcademicYear", "CreatedDate", "TrimesterNumber" },
                values: new object[] { "2025", new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9890), "2" });
        }
    }
}
