using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class addbranchingradation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "branchId",
                table: "EmployeeGradations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "statusId",
                table: "EmployeeGradations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_branchId",
                table: "EmployeeGradations",
                column: "branchId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeGradations_SpecialBranchUnits_branchId",
                table: "EmployeeGradations",
                column: "branchId",
                principalTable: "SpecialBranchUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeGradations_SpecialBranchUnits_branchId",
                table: "EmployeeGradations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeGradations_branchId",
                table: "EmployeeGradations");

            migrationBuilder.DropColumn(
                name: "branchId",
                table: "EmployeeGradations");

            migrationBuilder.DropColumn(
                name: "statusId",
                table: "EmployeeGradations");
        }
    }
}
