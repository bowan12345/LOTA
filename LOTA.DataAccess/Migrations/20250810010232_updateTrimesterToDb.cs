using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateTrimesterToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrimesterId",
                table: "StudentCourse",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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
                columns: new[] { "CreatedDate", "RegistrationDate", "TrimesterId" },
                values: new object[] { new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9907), new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9906), "Trimester-001" });

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
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9884));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9886));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9888));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 13, 2, 32, 217, DateTimeKind.Local).AddTicks(9890));

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_TrimesterId",
                table: "StudentCourse",
                column: "TrimesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_Trimester_TrimesterId",
                table: "StudentCourse",
                column: "TrimesterId",
                principalTable: "Trimester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_Trimester_TrimesterId",
                table: "StudentCourse");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourse_TrimesterId",
                table: "StudentCourse");

            migrationBuilder.DropColumn(
                name: "TrimesterId",
                table: "StudentCourse");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bdff6762-e87a-4c36-bf3a-01801846af35", "532b5e7f-4e52-418e-93c7-d3710c138108" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e00a1f9f-8117-4165-bb78-17134a2f0df9", "6eed43ad-c548-443a-b942-1507c8e4a769" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8076));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8014));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8018));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8055));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(7993));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8180), new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8178) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8154));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8156));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8157));
        }
    }
}
