using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class AssignmentAnulipiTableupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentAnulipis_AnulipiLists_anulipiId",
                table: "AssignmentAnulipis");

            migrationBuilder.DropColumn(
                name: "anulipId",
                table: "AssignmentAnulipis");

            migrationBuilder.RenameColumn(
                name: "anulipiId",
                table: "AssignmentAnulipis",
                newName: "anulipiListId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentAnulipis_anulipiId",
                table: "AssignmentAnulipis",
                newName: "IX_AssignmentAnulipis_anulipiListId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentAnulipis_AnulipiLists_anulipiListId",
                table: "AssignmentAnulipis",
                column: "anulipiListId",
                principalTable: "AnulipiLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentAnulipis_AnulipiLists_anulipiListId",
                table: "AssignmentAnulipis");

            migrationBuilder.RenameColumn(
                name: "anulipiListId",
                table: "AssignmentAnulipis",
                newName: "anulipiId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentAnulipis_anulipiListId",
                table: "AssignmentAnulipis",
                newName: "IX_AssignmentAnulipis_anulipiId");

            migrationBuilder.AddColumn<int>(
                name: "anulipId",
                table: "AssignmentAnulipis",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentAnulipis_AnulipiLists_anulipiId",
                table: "AssignmentAnulipis",
                column: "anulipiId",
                principalTable: "AnulipiLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
