using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class departmentalPromotionYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "departmentalPromotionYear",
                table: "EmployeeInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "extraActivitys",
                table: "EmployeeInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "extraSkill",
                table: "EmployeeInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "departmentalPromotionYear",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "extraActivitys",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "extraSkill",
                table: "EmployeeInfos");
        }
    }
}
