using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class addressnewdatamodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lat",
                table: "UpazilaInfos",
                type: "NVARCHAR(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lon",
                table: "UpazilaInfos",
                type: "NVARCHAR(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lat",
                table: "UnionInfos",
                type: "NVARCHAR(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lon",
                table: "UnionInfos",
                type: "NVARCHAR(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lat",
                table: "DistrictInfos",
                type: "NVARCHAR(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lon",
                table: "DistrictInfos",
                type: "NVARCHAR(20)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lat",
                table: "UpazilaInfos");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "UpazilaInfos");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "UnionInfos");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "UnionInfos");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "DistrictInfos");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "DistrictInfos");
        }
    }
}
