using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class postofficemodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "thanaId",
                table: "PostOffices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostOffices_thanaId",
                table: "PostOffices",
                column: "thanaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostOffices_Thanas_thanaId",
                table: "PostOffices",
                column: "thanaId",
                principalTable: "Thanas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostOffices_Thanas_thanaId",
                table: "PostOffices");

            migrationBuilder.DropIndex(
                name: "IX_PostOffices_thanaId",
                table: "PostOffices");

            migrationBuilder.DropColumn(
                name: "thanaId",
                table: "PostOffices");
        }
    }
}
