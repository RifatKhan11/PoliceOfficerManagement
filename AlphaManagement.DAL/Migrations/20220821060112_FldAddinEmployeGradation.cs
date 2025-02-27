using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class FldAddinEmployeGradation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpDistrictId",
                table: "EmployeeGradations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpDivisionId",
                table: "EmployeeGradations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpThanaId",
                table: "EmployeeGradations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_SpDistrictId",
                table: "EmployeeGradations",
                column: "SpDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_SpDivisionId",
                table: "EmployeeGradations",
                column: "SpDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_SpThanaId",
                table: "EmployeeGradations",
                column: "SpThanaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeGradations_Districts_SpDistrictId",
                table: "EmployeeGradations",
                column: "SpDistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeGradations_Divisions_SpDivisionId",
                table: "EmployeeGradations",
                column: "SpDivisionId",
                principalTable: "Divisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeGradations_Thanas_SpThanaId",
                table: "EmployeeGradations",
                column: "SpThanaId",
                principalTable: "Thanas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeGradations_Districts_SpDistrictId",
                table: "EmployeeGradations");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeGradations_Divisions_SpDivisionId",
                table: "EmployeeGradations");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeGradations_Thanas_SpThanaId",
                table: "EmployeeGradations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeGradations_SpDistrictId",
                table: "EmployeeGradations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeGradations_SpDivisionId",
                table: "EmployeeGradations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeGradations_SpThanaId",
                table: "EmployeeGradations");

            migrationBuilder.DropColumn(
                name: "SpDistrictId",
                table: "EmployeeGradations");

            migrationBuilder.DropColumn(
                name: "SpDivisionId",
                table: "EmployeeGradations");

            migrationBuilder.DropColumn(
                name: "SpThanaId",
                table: "EmployeeGradations");
        }
    }
}
