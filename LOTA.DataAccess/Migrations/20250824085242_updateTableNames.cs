using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOTA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_AssessmentType_AssessmentTypeId",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Course_CourseId",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_TrimesterCourse_CourseOfferingId",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Trimester_TrimesterId",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentLearningOutcome_Assignment_AssessmentId",
                table: "AssignmentLearningOutcome");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentLearningOutcome_LearningOutcome_LOId",
                table: "AssignmentLearningOutcome");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentScore_Assignment_AssessmentId",
                table: "StudentScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentLearningOutcome",
                table: "AssignmentLearningOutcome");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment");

            migrationBuilder.RenameTable(
                name: "AssignmentLearningOutcome",
                newName: "AssessmentLearningOutcome");

            migrationBuilder.RenameTable(
                name: "Assignment",
                newName: "Assessment");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentLearningOutcome_LOId",
                table: "AssessmentLearningOutcome",
                newName: "IX_AssessmentLearningOutcome_LOId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentLearningOutcome_AssessmentId",
                table: "AssessmentLearningOutcome",
                newName: "IX_AssessmentLearningOutcome_AssessmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_TrimesterId",
                table: "Assessment",
                newName: "IX_Assessment_TrimesterId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_CourseOfferingId",
                table: "Assessment",
                newName: "IX_Assessment_CourseOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_CourseId",
                table: "Assessment",
                newName: "IX_Assessment_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_AssessmentTypeId",
                table: "Assessment",
                newName: "IX_Assessment_AssessmentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssessmentLearningOutcome",
                table: "AssessmentLearningOutcome",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assessment",
                table: "Assessment",
                column: "Id");

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
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3829));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 52, 42, 501, DateTimeKind.Local).AddTicks(3832));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Assessment_AssessmentType_AssessmentTypeId",
                table: "Assessment",
                column: "AssessmentTypeId",
                principalTable: "AssessmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessment_Course_CourseId",
                table: "Assessment",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessment_TrimesterCourse_CourseOfferingId",
                table: "Assessment",
                column: "CourseOfferingId",
                principalTable: "TrimesterCourse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessment_Trimester_TrimesterId",
                table: "Assessment",
                column: "TrimesterId",
                principalTable: "Trimester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentLearningOutcome_Assessment_AssessmentId",
                table: "AssessmentLearningOutcome",
                column: "AssessmentId",
                principalTable: "Assessment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentLearningOutcome_LearningOutcome_LOId",
                table: "AssessmentLearningOutcome",
                column: "LOId",
                principalTable: "LearningOutcome",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScore_Assessment_AssessmentId",
                table: "StudentScore",
                column: "AssessmentId",
                principalTable: "Assessment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_AssessmentType_AssessmentTypeId",
                table: "Assessment");

            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_Course_CourseId",
                table: "Assessment");

            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_TrimesterCourse_CourseOfferingId",
                table: "Assessment");

            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_Trimester_TrimesterId",
                table: "Assessment");

            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentLearningOutcome_Assessment_AssessmentId",
                table: "AssessmentLearningOutcome");

            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentLearningOutcome_LearningOutcome_LOId",
                table: "AssessmentLearningOutcome");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentScore_Assessment_AssessmentId",
                table: "StudentScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssessmentLearningOutcome",
                table: "AssessmentLearningOutcome");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assessment",
                table: "Assessment");

            migrationBuilder.RenameTable(
                name: "AssessmentLearningOutcome",
                newName: "AssignmentLearningOutcome");

            migrationBuilder.RenameTable(
                name: "Assessment",
                newName: "Assignment");

            migrationBuilder.RenameIndex(
                name: "IX_AssessmentLearningOutcome_LOId",
                table: "AssignmentLearningOutcome",
                newName: "IX_AssignmentLearningOutcome_LOId");

            migrationBuilder.RenameIndex(
                name: "IX_AssessmentLearningOutcome_AssessmentId",
                table: "AssignmentLearningOutcome",
                newName: "IX_AssignmentLearningOutcome_AssessmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Assessment_TrimesterId",
                table: "Assignment",
                newName: "IX_Assignment_TrimesterId");

            migrationBuilder.RenameIndex(
                name: "IX_Assessment_CourseOfferingId",
                table: "Assignment",
                newName: "IX_Assignment_CourseOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Assessment_CourseId",
                table: "Assignment",
                newName: "IX_Assignment_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Assessment_AssessmentTypeId",
                table: "Assignment",
                newName: "IX_Assignment_AssessmentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentLearningOutcome",
                table: "AssignmentLearningOutcome",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "STUDENT-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d57e8e6b-b801-4df4-afd6-ed94f10289d3", "c58e0d88-8c34-49d1-82f9-5ac86811f2fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "TUTOR-001",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b87089ef-8e51-43a0-b5a9-16b5e46480f9", "d216d9ca-492d-485f-97f9-048e80029b32" });

            migrationBuilder.UpdateData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: "ASSIGN-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(783));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(645));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: "COURSE-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(649));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(738));

            migrationBuilder.UpdateData(
                table: "LearningOutcome",
                keyColumn: "Id",
                keyValue: "LO-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(741));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(556));

            migrationBuilder.UpdateData(
                table: "Qualification",
                keyColumn: "Id",
                keyValue: "Qualification-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(620));

            migrationBuilder.UpdateData(
                table: "StudentCourse",
                keyColumn: "Id",
                keyValue: "STCOURSE-001",
                columns: new[] { "CreatedDate", "RegistrationDate" },
                values: new object[] { new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(817), new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(815) });

            migrationBuilder.UpdateData(
                table: "StudentScore",
                keyColumn: "Id",
                keyValue: "SCORE-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(843));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(691));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-002",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(694));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-003",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(696));

            migrationBuilder.UpdateData(
                table: "Trimester",
                keyColumn: "Id",
                keyValue: "Trimester-004",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(698));

            migrationBuilder.UpdateData(
                table: "TrimesterCourse",
                keyColumn: "Id",
                keyValue: "TC001",
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 20, 49, 55, 68, DateTimeKind.Local).AddTicks(717));

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_AssessmentType_AssessmentTypeId",
                table: "Assignment",
                column: "AssessmentTypeId",
                principalTable: "AssessmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Course_CourseId",
                table: "Assignment",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_TrimesterCourse_CourseOfferingId",
                table: "Assignment",
                column: "CourseOfferingId",
                principalTable: "TrimesterCourse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Trimester_TrimesterId",
                table: "Assignment",
                column: "TrimesterId",
                principalTable: "Trimester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentLearningOutcome_Assignment_AssessmentId",
                table: "AssignmentLearningOutcome",
                column: "AssessmentId",
                principalTable: "Assignment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentLearningOutcome_LearningOutcome_LOId",
                table: "AssignmentLearningOutcome",
                column: "LOId",
                principalTable: "LearningOutcome",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScore_Assignment_AssessmentId",
                table: "StudentScore",
                column: "AssessmentId",
                principalTable: "Assignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
