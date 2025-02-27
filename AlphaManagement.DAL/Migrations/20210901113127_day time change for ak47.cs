using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class daytimechangeforak47 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dayTime",
                table: "Assignments");

            migrationBuilder.AddColumn<int>(
                name: "joinDayTime",
                table: "Assignments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "releaseDayTime",
                table: "Assignments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "joinDayTime",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "releaseDayTime",
                table: "Assignments");

            migrationBuilder.AddColumn<int>(
                name: "dayTime",
                table: "Assignments",
                nullable: true);
        }
    }
}
