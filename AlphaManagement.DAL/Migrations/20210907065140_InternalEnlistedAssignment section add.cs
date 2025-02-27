using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class InternalEnlistedAssignmentsectionadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalEnlistedAssignments_SpecialBranchUnits_specialBranchUnitId",
                table: "InternalEnlistedAssignments");

            migrationBuilder.RenameColumn(
                name: "specialBranchUnitId",
                table: "InternalEnlistedAssignments",
                newName: "sectionId");

            migrationBuilder.RenameIndex(
                name: "IX_InternalEnlistedAssignments_specialBranchUnitId",
                table: "InternalEnlistedAssignments",
                newName: "IX_InternalEnlistedAssignments_sectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalEnlistedAssignments_Departments_sectionId",
                table: "InternalEnlistedAssignments",
                column: "sectionId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalEnlistedAssignments_Departments_sectionId",
                table: "InternalEnlistedAssignments");

            migrationBuilder.RenameColumn(
                name: "sectionId",
                table: "InternalEnlistedAssignments",
                newName: "specialBranchUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_InternalEnlistedAssignments_sectionId",
                table: "InternalEnlistedAssignments",
                newName: "IX_InternalEnlistedAssignments_specialBranchUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalEnlistedAssignments_SpecialBranchUnits_specialBranchUnitId",
                table: "InternalEnlistedAssignments",
                column: "specialBranchUnitId",
                principalTable: "SpecialBranchUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
