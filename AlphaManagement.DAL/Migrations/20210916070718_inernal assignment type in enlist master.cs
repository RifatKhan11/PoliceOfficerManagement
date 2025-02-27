using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class inernalassignmenttypeinenlistmaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "assignmentTypeId",
                table: "InternalEnlistedAssignmentMasters",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternalEnlistedAssignmentMasters_assignmentTypeId",
                table: "InternalEnlistedAssignmentMasters",
                column: "assignmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalEnlistedAssignmentMasters_InternalAssignmentTypes_assignmentTypeId",
                table: "InternalEnlistedAssignmentMasters",
                column: "assignmentTypeId",
                principalTable: "InternalAssignmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalEnlistedAssignmentMasters_InternalAssignmentTypes_assignmentTypeId",
                table: "InternalEnlistedAssignmentMasters");

            migrationBuilder.DropIndex(
                name: "IX_InternalEnlistedAssignmentMasters_assignmentTypeId",
                table: "InternalEnlistedAssignmentMasters");

            migrationBuilder.DropColumn(
                name: "assignmentTypeId",
                table: "InternalEnlistedAssignmentMasters");
        }
    }
}
