using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class addfieldinDisciplinaryactiontable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OffenseName",
                table: "DisciplinaryActions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PunishmentName",
                table: "DisciplinaryActions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OffenseName",
                table: "DisciplinaryActions");

            migrationBuilder.DropColumn(
                name: "PunishmentName",
                table: "DisciplinaryActions");
        }
    }
}
