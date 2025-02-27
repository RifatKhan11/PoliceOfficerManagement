using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class orgaddingradation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "organizationId",
                table: "EmployeeGradations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_organizationId",
                table: "EmployeeGradations",
                column: "organizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeGradations_Organizations_organizationId",
                table: "EmployeeGradations",
                column: "organizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeGradations_Organizations_organizationId",
                table: "EmployeeGradations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeGradations_organizationId",
                table: "EmployeeGradations");

            migrationBuilder.DropColumn(
                name: "organizationId",
                table: "EmployeeGradations");
        }
    }
}
