using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class enum_assign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "articleStatus",
                table: "Assignments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "articleType",
                table: "Assignments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dayTime",
                table: "Assignments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "articleStatus",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "articleType",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "dayTime",
                table: "Assignments");
        }
    }
}
