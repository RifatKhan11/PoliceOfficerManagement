using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class EmployeeGradationsFldAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "degreeId",
                table: "EmployeeGradations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_degreeId",
                table: "EmployeeGradations",
                column: "degreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeGradations_Degrees_degreeId",
                table: "EmployeeGradations",
                column: "degreeId",
                principalTable: "Degrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeGradations_Degrees_degreeId",
                table: "EmployeeGradations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeGradations_degreeId",
                table: "EmployeeGradations");

            migrationBuilder.DropColumn(
                name: "degreeId",
                table: "EmployeeGradations");
        }
    }
}
