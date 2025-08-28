using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addALOToLOScoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssessmentLearningOutcomeId",
                table: "StudentLOScore",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "92581e60-a57b-46a5-99d6-562b7946164e", "de0485fc-1716-4db7-9ec5-776c00361b61" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "973d45dd-4578-46be-811b-c5efc0ebb603", "ae88cca8-3499-4e90-baca-21c533c76626" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8074));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8159));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8161));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(7995));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8048));

            migrationBuilder.UpdateData(
                table: "StudentAssessmentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8247));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8230), new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8229) });

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-001",
                columns: new[] { "AssessmentLearningOutcomeId", "CreatedDate" },
                values: new object[] { "ALO1001", new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8263) });

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-002",
                columns: new[] { "AssessmentLearningOutcomeId", "CreatedDate" },
                values: new object[] { "ALO1002", new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8265) });

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8113));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8115));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8117));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8118));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 28, 10, 32, 52, 315, DateTimeKind.Local).AddTicks(8139));

            migrationBuilder.CreateIndex(
                name: "IX_StudentLOScore_AssessmentLearningOutcomeId",
                table: "StudentLOScore",
                column: "AssessmentLearningOutcomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLOScore_AssessmentLearningOutcome_AssessmentLearningOutcomeId",
                table: "StudentLOScore",
                column: "AssessmentLearningOutcomeId",
                principalTable: "AssessmentLearningOutcome",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLOScore_AssessmentLearningOutcome_AssessmentLearningOutcomeId",
                table: "StudentLOScore");

            migrationBuilder.DropIndex(
                name: "IX_StudentLOScore_AssessmentLearningOutcomeId",
                table: "StudentLOScore");

            migrationBuilder.DropColumn(
                name: "AssessmentLearningOutcomeId",
                table: "StudentLOScore");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ea637cb3-a144-4857-96d0-a80ae3c9b594", "887d4f53-da4f-408d-b6f4-a3281a71d6c0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bb00736b-a043-4e9d-98c1-cfeaa0e48708", "2bca5028-5672-4df8-af1e-8ed026c824a0" });

            migrationBuilder.UpdateData(
                table: "Assessment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1961));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1842));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1920));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1922));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1767));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1818));

            migrationBuilder.UpdateData(
                table: "StudentAssessmentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(2015));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1999), new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1998) });

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(2028));

            migrationBuilder.UpdateData(
                table: "StudentLOScore",
                keyColumn: "Id",
                keyValue: "LOSCORE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(2030));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1878));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1880));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1882));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1884));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 23, 22, 29, 782, DateTimeKind.Local).AddTicks(1901));
        }
    }
}
