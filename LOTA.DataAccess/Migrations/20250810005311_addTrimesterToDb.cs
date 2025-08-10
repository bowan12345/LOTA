using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addTrimesterToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrimesterId",
                table: "StudentScore",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Trimester",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrimesterNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trimester", x => x.Id);
                });

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
                columns: new[] { "CreatedDate", "TrimesterId" },
                values: new object[] { new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8197), "Trimester-001" });

            migrationBuilder.InsertData(
                table: "Trimester",
                columns: new[] { "Id", "AcademicYear", "CreatedDate", "IsActive", "TrimesterNumber", "UpdatedDate" },
                values: new object[,]
                {
                    { "Trimester-001", "2024", new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8152), true, "1", null },
                    { "Trimester-002", "2024", new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8154), true, "2", null },
                    { "Trimester-003", "2025", new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8156), true, "1", null },
                    { "Trimester-004", "2025", new DateTime(2025, 8, 10, 12, 53, 8, 217, DateTimeKind.Local).AddTicks(8157), true, "2", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentScore_TrimesterId",
                table: "StudentScore",
                column: "TrimesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScore_Trimester_TrimesterId",
                table: "StudentScore",
                column: "TrimesterId",
                principalTable: "Trimester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentScore_Trimester_TrimesterId",
                table: "StudentScore");

            migrationBuilder.DropTable(
                name: "Trimester");

            migrationBuilder.DropIndex(
                name: "IX_StudentScore_TrimesterId",
                table: "StudentScore");

            migrationBuilder.DropColumn(
                name: "TrimesterId",
                table: "StudentScore");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f91fb543-a28a-4d0d-93da-ebdfd6f4e2c7", "9489b9ec-001d-4c69-9717-a4e94aa93303" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "88cacf1a-cc4e-4f03-8c6d-a7bce3a4545d", "f3903796-6943-4b88-9b3c-6c58f1bab8ce" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 35, 50, 902, DateTimeKind.Local).AddTicks(4227));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 35, 50, 902, DateTimeKind.Local).AddTicks(4166));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 35, 50, 902, DateTimeKind.Local).AddTicks(4169));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 35, 50, 902, DateTimeKind.Local).AddTicks(4203));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 35, 50, 902, DateTimeKind.Local).AddTicks(4206));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 35, 50, 902, DateTimeKind.Local).AddTicks(4092));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 35, 50, 902, DateTimeKind.Local).AddTicks(4146));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 9, 13, 35, 50, 902, DateTimeKind.Local).AddTicks(4255), new DateTime(2025, 8, 9, 13, 35, 50, 902, DateTimeKind.Local).AddTicks(4254) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 13, 35, 50, 902, DateTimeKind.Local).AddTicks(4269));
        }
    }
}
