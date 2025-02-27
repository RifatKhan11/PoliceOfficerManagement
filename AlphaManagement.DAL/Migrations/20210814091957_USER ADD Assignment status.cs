using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class USERADDAssignmentstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "AssignmentStatusLogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentStatusLogs_applicationUserId",
                table: "AssignmentStatusLogs",
                column: "applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentStatusLogs_AspNetUsers_applicationUserId",
                table: "AssignmentStatusLogs",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentStatusLogs_AspNetUsers_applicationUserId",
                table: "AssignmentStatusLogs");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentStatusLogs_applicationUserId",
                table: "AssignmentStatusLogs");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "AssignmentStatusLogs");
        }
    }
}
