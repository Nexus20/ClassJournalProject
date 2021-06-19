using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassJournalProject.Data.Migrations
{
    public partial class TeacherRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Teachers");
        }
    }
}
