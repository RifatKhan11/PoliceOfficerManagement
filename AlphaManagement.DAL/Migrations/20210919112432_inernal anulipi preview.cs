using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class inernalanulipipreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "internalAssignMasterId",
                table: "AssignmentAnulipiPreviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAnulipiPreviews_internalAssignMasterId",
                table: "AssignmentAnulipiPreviews",
                column: "internalAssignMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentAnulipiPreviews_InternalAssignmentMasters_internalAssignMasterId",
                table: "AssignmentAnulipiPreviews",
                column: "internalAssignMasterId",
                principalTable: "InternalAssignmentMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentAnulipiPreviews_InternalAssignmentMasters_internalAssignMasterId",
                table: "AssignmentAnulipiPreviews");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentAnulipiPreviews_internalAssignMasterId",
                table: "AssignmentAnulipiPreviews");

            migrationBuilder.DropColumn(
                name: "internalAssignMasterId",
                table: "AssignmentAnulipiPreviews");
        }
    }
}
