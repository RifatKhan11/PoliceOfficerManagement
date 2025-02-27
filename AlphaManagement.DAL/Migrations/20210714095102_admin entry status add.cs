using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class adminentrystatusadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "isAdminEntry",
                table: "TraningLogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isAdminEntry",
                table: "Spouses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isAdminEntry",
                table: "PromotionLogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isAdminEntry",
                table: "ForeignTravels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isAdminEntry",
                table: "EmployeeInfos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isAdminEntry",
                table: "EducationalQualifications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isAdminEntry",
                table: "AwardEntries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isAdminEntry",
                table: "Assignments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isAdminEntry",
                table: "AddressInformation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAdminEntry",
                table: "TraningLogs");

            migrationBuilder.DropColumn(
                name: "isAdminEntry",
                table: "Spouses");

            migrationBuilder.DropColumn(
                name: "isAdminEntry",
                table: "PromotionLogs");

            migrationBuilder.DropColumn(
                name: "isAdminEntry",
                table: "ForeignTravels");

            migrationBuilder.DropColumn(
                name: "isAdminEntry",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "isAdminEntry",
                table: "EducationalQualifications");

            migrationBuilder.DropColumn(
                name: "isAdminEntry",
                table: "AwardEntries");

            migrationBuilder.DropColumn(
                name: "isAdminEntry",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "isAdminEntry",
                table: "AddressInformation");
        }
    }
}
