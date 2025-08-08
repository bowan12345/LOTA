using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addQualificationTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QualificationId",
                table: "Course",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QualificationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    QualificationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bef03561-b91b-4b0f-8185-0e43c2641a15", "ae4d2661-8707-4d8f-9093-256d27aea840" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "adfef29f-159f-47c0-b527-79772b1e9b7e", "0b1d6ac5-198e-4ab9-b854-1014fa0abad6" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 11, 34, 4, 487, DateTimeKind.Local).AddTicks(9760));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                columns: new[] { "CreatedDate", "Level", "QualificationId" },
                values: new object[] { new DateTime(2025, 8, 9, 11, 34, 4, 487, DateTimeKind.Local).AddTicks(9648), 5, "Qualification-001" });

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                columns: new[] { "CreatedDate", "Level", "QualificationId" },
                values: new object[] { new DateTime(2025, 8, 9, 11, 34, 4, 487, DateTimeKind.Local).AddTicks(9652), 5, "Qualification-002" });

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 11, 34, 4, 487, DateTimeKind.Local).AddTicks(9735));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 11, 34, 4, 487, DateTimeKind.Local).AddTicks(9739));

            migrationBuilder.InsertData(
                table: "Qualification",
                columns: new[] { "Id", "CreatedDate", "IsActive", "QualificationName", "QualificationType", "UpdatedDate" },
                values: new object[,]
                {
                    { "Qualification-001", new DateTime(2025, 8, 9, 11, 34, 4, 487, DateTimeKind.Local).AddTicks(9561), true, "Bachelor of Information Technolog", "Bachelor", null },
                    { "Qualification-002", new DateTime(2025, 8, 9, 11, 34, 4, 487, DateTimeKind.Local).AddTicks(9618), true, "Diploma in IT Technical Support", "Diploma", null }
                });

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 9, 11, 34, 4, 487, DateTimeKind.Local).AddTicks(9796), new DateTime(2025, 8, 9, 11, 34, 4, 487, DateTimeKind.Local).AddTicks(9794) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 9, 11, 34, 4, 487, DateTimeKind.Local).AddTicks(9811));

            migrationBuilder.CreateIndex(
                name: "IX_Course_QualificationId",
                table: "Course",
                column: "QualificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Qualification_QualificationId",
                table: "Course",
                column: "QualificationId",
                principalTable: "Qualification",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Qualification_QualificationId",
                table: "Course");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropIndex(
                name: "IX_Course_QualificationId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "QualificationId",
                table: "Course");

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
        }
    }
}
