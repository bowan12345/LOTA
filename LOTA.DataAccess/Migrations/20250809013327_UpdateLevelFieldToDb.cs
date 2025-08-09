using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLevelFieldToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Qualification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f626d084-4c03-4bb7-81e7-c62988326141", "a3f1fd72-66bc-4462-adba-dfa16edf743b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d3c9984a-9eef-4a37-91a0-f6461b0734e2", "d28cda06-4643-40da-a4ba-7b017df87f58" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 33, 24, 155, DateTimeKind.Local).AddTicks(8453));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                columns: new[] { "CreatedDate", "Level" },
                values: new object[] { new DateTime(2025, 8, 9, 13, 33, 24, 155, DateTimeKind.Local).AddTicks(8391), 0 });

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                columns: new[] { "CreatedDate", "Level" },
                values: new object[] { new DateTime(2025, 8, 9, 13, 33, 24, 155, DateTimeKind.Local).AddTicks(8394), 0 });

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 33, 24, 155, DateTimeKind.Local).AddTicks(8430));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 33, 24, 155, DateTimeKind.Local).AddTicks(8434));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                columns: new[] { "CreatedDate", "Level" },
                values: new object[] { new DateTime(2025, 8, 9, 13, 33, 24, 155, DateTimeKind.Local).AddTicks(8317), 7 });

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                columns: new[] { "CreatedDate", "Level" },
                values: new object[] { new DateTime(2025, 8, 9, 13, 33, 24, 155, DateTimeKind.Local).AddTicks(8369), 5 });

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 9, 13, 33, 24, 155, DateTimeKind.Local).AddTicks(8485), new DateTime(2025, 8, 9, 13, 33, 24, 155, DateTimeKind.Local).AddTicks(8484) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 33, 24, 155, DateTimeKind.Local).AddTicks(8518));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Qualification");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2d923109-9328-4a31-9f6b-9c4e71a5d9e6", "0793daa0-843e-4b3a-9f55-e492f19da2eb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "01d470d8-ddad-4162-8f0f-ec40ef570161", "15799246-b31e-4f5e-8499-0ead361ac4da" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 12, 39, 46, 681, DateTimeKind.Local).AddTicks(6154));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                columns: new[] { "CreatedDate", "Level" },
                values: new object[] { new DateTime(2025, 8, 9, 12, 39, 46, 681, DateTimeKind.Local).AddTicks(6081), 5 });

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                columns: new[] { "CreatedDate", "Level" },
                values: new object[] { new DateTime(2025, 8, 9, 12, 39, 46, 681, DateTimeKind.Local).AddTicks(6085), 5 });

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 12, 39, 46, 681, DateTimeKind.Local).AddTicks(6129));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 12, 39, 46, 681, DateTimeKind.Local).AddTicks(6132));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 12, 39, 46, 681, DateTimeKind.Local).AddTicks(6004));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 12, 39, 46, 681, DateTimeKind.Local).AddTicks(6059));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 9, 12, 39, 46, 681, DateTimeKind.Local).AddTicks(6196), new DateTime(2025, 8, 9, 12, 39, 46, 681, DateTimeKind.Local).AddTicks(6195) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 12, 39, 46, 681, DateTimeKind.Local).AddTicks(6216));
        }
    }
}
