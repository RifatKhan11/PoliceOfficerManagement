using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class anulipimigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "anulipiText",
                table: "AssignmentAnulipis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "shortOrder",
                table: "AssignmentAnulipis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "anulipiText",
                table: "AssignmentAnulipis");

            migrationBuilder.DropColumn(
                name: "shortOrder",
                table: "AssignmentAnulipis");
        }
    }
}
