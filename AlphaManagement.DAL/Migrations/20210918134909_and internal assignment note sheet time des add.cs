using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class andinternalassignmentnotesheettimedesadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "noteSheetDescription",
                table: "InternalAssignmentMasters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "noteSheetTitle",
                table: "InternalAssignmentMasters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "noteSheetDescription",
                table: "InternalAssignmentMasters");

            migrationBuilder.DropColumn(
                name: "noteSheetTitle",
                table: "InternalAssignmentMasters");
        }
    }
}
