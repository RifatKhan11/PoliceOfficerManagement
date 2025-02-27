using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class newaddressmodify_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lon",
                table: "DistrictInfos",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "DistrictInfos",
                newName: "latitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "DistrictInfos",
                newName: "lon");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "DistrictInfos",
                newName: "lat");
        }
    }
}
