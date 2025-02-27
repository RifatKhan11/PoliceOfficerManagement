using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class empfieldadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "extraActivity",
                table: "EmployeeInfos",
                type: "nvarchar(350)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sectionName",
                table: "EmployeeInfos",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "skill",
                table: "EmployeeInfos",
                type: "nvarchar(350)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sectionName",
                table: "Assignments",
                type: "NVARCHAR(150)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "extraActivity",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "sectionName",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "skill",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "sectionName",
                table: "Assignments");
        }
    }
}
