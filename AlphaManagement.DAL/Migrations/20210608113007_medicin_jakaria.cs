using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class medicin_jakaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CVRMedicalInjury",
                table: "MedicalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "year",
                table: "MedicalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "referenceNumber",
                table: "ForeignTravels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "travelDuration",
                table: "ForeignTravels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVRMedicalInjury",
                table: "MedicalInfos");

            migrationBuilder.DropColumn(
                name: "year",
                table: "MedicalInfos");

            migrationBuilder.DropColumn(
                name: "referenceNumber",
                table: "ForeignTravels");

            migrationBuilder.DropColumn(
                name: "travelDuration",
                table: "ForeignTravels");
        }
    }
}
