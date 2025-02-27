using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class andinternalenlishtidininternalassignmentmaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "internalEnlistedAssignmentMasterId",
                table: "InternalAssignmentMasters",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentMasters_internalEnlistedAssignmentMasterId",
                table: "InternalAssignmentMasters",
                column: "internalEnlistedAssignmentMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalAssignmentMasters_InternalEnlistedAssignmentMasters_internalEnlistedAssignmentMasterId",
                table: "InternalAssignmentMasters",
                column: "internalEnlistedAssignmentMasterId",
                principalTable: "InternalEnlistedAssignmentMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalAssignmentMasters_InternalEnlistedAssignmentMasters_internalEnlistedAssignmentMasterId",
                table: "InternalAssignmentMasters");

            migrationBuilder.DropIndex(
                name: "IX_InternalAssignmentMasters_internalEnlistedAssignmentMasterId",
                table: "InternalAssignmentMasters");

            migrationBuilder.DropColumn(
                name: "internalEnlistedAssignmentMasterId",
                table: "InternalAssignmentMasters");
        }
    }
}
