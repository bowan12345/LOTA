using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addGraduateDiplomaToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9c88e051-6119-44b2-a4ad-cd1218ee1bc4", "b49e2800-3247-48d1-9fe7-e3a80a282f56" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9d75eaad-ded4-49b9-a073-25381bbcd304", "e35320e2-23be-4ad6-a880-591678d664b2" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6681));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6613));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6616));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6659));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6661));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6516));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6566));

            migrationBuilder.UpdateData(
                table: "QualificationType",
                keyColumn: "Id",
                keyValue: "007",
                column: "QualificationTypeName",
                value: "Postgraduate Diploma");

            migrationBuilder.InsertData(
                table: "QualificationType",
                columns: new[] { "Id", "QualificationTypeName" },
                values: new object[] { "008", "Postgraduate Certificate" });

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6737), new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6736) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6754));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6713));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6716));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6717));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 23, 10, 49, 7, 608, DateTimeKind.Local).AddTicks(6719));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QualificationType",
                keyColumn: "Id",
                keyValue: "008");

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
                table: "QualificationType",
                keyColumn: "Id",
                keyValue: "007",
                column: "QualificationTypeName",
                value: "Postgraduate Certificate");

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
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7982));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7984));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7986));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 10, 17, 1, 52, 58, DateTimeKind.Local).AddTicks(7988));
        }
    }
}
