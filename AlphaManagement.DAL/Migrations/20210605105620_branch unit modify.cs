using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class branchunitmodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "isParent",
                table: "SpecialBranchUnits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "specialBranchUnitId",
                table: "SpecialBranchUnits",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialBranchUnits_specialBranchUnitId",
                table: "SpecialBranchUnits",
                column: "specialBranchUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialBranchUnits_SpecialBranchUnits_specialBranchUnitId",
                table: "SpecialBranchUnits",
                column: "specialBranchUnitId",
                principalTable: "SpecialBranchUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialBranchUnits_SpecialBranchUnits_specialBranchUnitId",
                table: "SpecialBranchUnits");

            migrationBuilder.DropIndex(
                name: "IX_SpecialBranchUnits_specialBranchUnitId",
                table: "SpecialBranchUnits");

            migrationBuilder.DropColumn(
                name: "isParent",
                table: "SpecialBranchUnits");

            migrationBuilder.DropColumn(
                name: "specialBranchUnitId",
                table: "SpecialBranchUnits");
        }
    }
}
