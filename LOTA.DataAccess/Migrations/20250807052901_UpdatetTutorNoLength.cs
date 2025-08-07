using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatetTutorNoLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorCourse_Course_CourseId",
                table: "TutorCourse");

            migrationBuilder.AlterColumn<string>(
                name: "TutorNo",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentNo",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f8b15424-db21-4fba-a418-43608ea79135", "7847fdb2-7d32-4843-ba54-2642ceaa73b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "dabaac0e-4e00-4677-b798-719636cb2133", "5b058dd4-8483-430f-a113-f5c139615929" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 17, 28, 57, 265, DateTimeKind.Local).AddTicks(6519));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 17, 28, 57, 265, DateTimeKind.Local).AddTicks(6416));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 17, 28, 57, 265, DateTimeKind.Local).AddTicks(6461));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 17, 28, 57, 265, DateTimeKind.Local).AddTicks(6500));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 17, 28, 57, 265, DateTimeKind.Local).AddTicks(6503));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 7, 17, 28, 57, 265, DateTimeKind.Local).AddTicks(6552), new DateTime(2025, 8, 7, 17, 28, 57, 265, DateTimeKind.Local).AddTicks(6551) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 17, 28, 57, 265, DateTimeKind.Local).AddTicks(6567));

            migrationBuilder.AddForeignKey(
                name: "FK_TutorCourse_Course_CourseId",
                table: "TutorCourse",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorCourse_Course_CourseId",
                table: "TutorCourse");

            migrationBuilder.AlterColumn<string>(
                name: "TutorNo",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentNo",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d90a5709-01d1-48e6-81fc-244182e185ec", "5701eeb3-0e70-4976-bee3-fc2d64acc811" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_TutorCourse_Course_CourseId",
                table: "TutorCourse",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");
        }
    }
}
