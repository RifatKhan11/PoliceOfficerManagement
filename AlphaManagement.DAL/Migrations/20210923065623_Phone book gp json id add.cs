using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class Phonebookgpjsonidadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "jsonId",
                table: "PhoneBookUnits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "jsonId",
                table: "PhoneBooks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "jsonId",
                table: "PhoneBookRanks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "jsonId",
                table: "PhoneBookUnits");

            migrationBuilder.DropColumn(
                name: "jsonId",
                table: "PhoneBooks");

            migrationBuilder.DropColumn(
                name: "jsonId",
                table: "PhoneBookRanks");
        }
    }
}
