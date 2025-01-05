using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoliceOfficerManagement.Migrations
{
    /// <inheritdoc />
    public partial class tablerankAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PostingPlaces_rankId",
                table: "PostingPlaces",
                column: "rankId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostingPlaces_ranks_rankId",
                table: "PostingPlaces",
                column: "rankId",
                principalTable: "ranks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostingPlaces_ranks_rankId",
                table: "PostingPlaces");

            migrationBuilder.DropIndex(
                name: "IX_PostingPlaces_rankId",
                table: "PostingPlaces");
        }
    }
}
