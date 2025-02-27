using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class BDPolicePhoneBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sectionId",
                table: "Departments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_sectionId",
                table: "Departments",
                column: "sectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Sections_sectionId",
                table: "Departments",
                column: "sectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Sections_sectionId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_sectionId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "sectionId",
                table: "Departments");
        }
    }
}
