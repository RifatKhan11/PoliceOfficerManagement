using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class awardRelInAwardEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "awardId",
                table: "AwardEntries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AwardEntries_awardId",
                table: "AwardEntries",
                column: "awardId");

            migrationBuilder.AddForeignKey(
                name: "FK_AwardEntries_Awards_awardId",
                table: "AwardEntries",
                column: "awardId",
                principalTable: "Awards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AwardEntries_Awards_awardId",
                table: "AwardEntries");

            migrationBuilder.DropIndex(
                name: "IX_AwardEntries_awardId",
                table: "AwardEntries");

            migrationBuilder.DropColumn(
                name: "awardId",
                table: "AwardEntries");
        }
    }
}
