using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class designationadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "designationName",
                table: "Assignments",
                type: "NVARCHAR(350)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unitName",
                table: "Assignments",
                type: "NVARCHAR(350)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "designationName",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "unitName",
                table: "Assignments");
        }
    }
}
