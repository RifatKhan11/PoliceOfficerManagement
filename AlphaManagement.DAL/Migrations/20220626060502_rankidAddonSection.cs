using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class rankidAddonSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rankId",
                table: "Sections",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_rankId",
                table: "Sections",
                column: "rankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Ranks_rankId",
                table: "Sections",
                column: "rankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Ranks_rankId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_rankId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "rankId",
                table: "Sections");
        }
    }
}
