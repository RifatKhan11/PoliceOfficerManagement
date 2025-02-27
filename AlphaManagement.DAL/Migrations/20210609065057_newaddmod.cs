using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class newaddmod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnionInfos_DistrictInfos_districtId",
                table: "UnionInfos");

            migrationBuilder.RenameColumn(
                name: "districtId",
                table: "UnionInfos",
                newName: "upazilaId");

            migrationBuilder.RenameIndex(
                name: "IX_UnionInfos_districtId",
                table: "UnionInfos",
                newName: "IX_UnionInfos_upazilaId");

            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "DistrictInfos",
                newName: "lon");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "DistrictInfos",
                newName: "lat");

            migrationBuilder.AddColumn<int>(
                name: "districtId",
                table: "UpazilaInfos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UpazilaInfos_districtId",
                table: "UpazilaInfos",
                column: "districtId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnionInfos_UpazilaInfos_upazilaId",
                table: "UnionInfos",
                column: "upazilaId",
                principalTable: "UpazilaInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UpazilaInfos_DistrictInfos_districtId",
                table: "UpazilaInfos",
                column: "districtId",
                principalTable: "DistrictInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnionInfos_UpazilaInfos_upazilaId",
                table: "UnionInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UpazilaInfos_DistrictInfos_districtId",
                table: "UpazilaInfos");

            migrationBuilder.DropIndex(
                name: "IX_UpazilaInfos_districtId",
                table: "UpazilaInfos");

            migrationBuilder.DropColumn(
                name: "districtId",
                table: "UpazilaInfos");

            migrationBuilder.RenameColumn(
                name: "upazilaId",
                table: "UnionInfos",
                newName: "districtId");

            migrationBuilder.RenameIndex(
                name: "IX_UnionInfos_upazilaId",
                table: "UnionInfos",
                newName: "IX_UnionInfos_districtId");

            migrationBuilder.RenameColumn(
                name: "lon",
                table: "DistrictInfos",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "DistrictInfos",
                newName: "latitude");

            migrationBuilder.AddForeignKey(
                name: "FK_UnionInfos_DistrictInfos_districtId",
                table: "UnionInfos",
                column: "districtId",
                principalTable: "DistrictInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
