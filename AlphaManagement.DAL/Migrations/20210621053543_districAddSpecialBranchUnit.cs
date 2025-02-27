using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class districAddSpecialBranchUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "districtsId",
                table: "SpecialBranchUnits",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialBranchUnits_districtsId",
                table: "SpecialBranchUnits",
                column: "districtsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialBranchUnits_Districts_districtsId",
                table: "SpecialBranchUnits",
                column: "districtsId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialBranchUnits_Districts_districtsId",
                table: "SpecialBranchUnits");

            migrationBuilder.DropIndex(
                name: "IX_SpecialBranchUnits_districtsId",
                table: "SpecialBranchUnits");

            migrationBuilder.DropColumn(
                name: "districtsId",
                table: "SpecialBranchUnits");
        }
    }
}
