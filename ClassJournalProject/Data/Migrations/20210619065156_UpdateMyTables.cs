using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassJournalProject.Data.Migrations
{
    public partial class UpdateMyTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtySubjectAssignments_Specialties_SpecialtyId",
                table: "SpecialtySubjectAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtySubjectAssignments_Subjects_SubjectId",
                table: "SpecialtySubjectAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjectAssignments_Subjects_SubjectId",
                table: "TeacherSubjectAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjectAssignments_Teachers_TeacherId",
                table: "TeacherSubjectAssignments");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtySubjectAssignments_Specialties_SpecialtyId",
                table: "SpecialtySubjectAssignments",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtySubjectAssignments_Subjects_SubjectId",
                table: "SpecialtySubjectAssignments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjectAssignments_Subjects_SubjectId",
                table: "TeacherSubjectAssignments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjectAssignments_Teachers_TeacherId",
                table: "TeacherSubjectAssignments",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtySubjectAssignments_Specialties_SpecialtyId",
                table: "SpecialtySubjectAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtySubjectAssignments_Subjects_SubjectId",
                table: "SpecialtySubjectAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjectAssignments_Subjects_SubjectId",
                table: "TeacherSubjectAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjectAssignments_Teachers_TeacherId",
                table: "TeacherSubjectAssignments");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtySubjectAssignments_Specialties_SpecialtyId",
                table: "SpecialtySubjectAssignments",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtySubjectAssignments_Subjects_SubjectId",
                table: "SpecialtySubjectAssignments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjectAssignments_Subjects_SubjectId",
                table: "TeacherSubjectAssignments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjectAssignments_Teachers_TeacherId",
                table: "TeacherSubjectAssignments",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
