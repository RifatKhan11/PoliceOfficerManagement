using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class subunitbranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "districtId",
                table: "Spouses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubBranchUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    specialBranchUnitId = table.Column<int>(nullable: true),
                    branchUnitName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    branchUnitNameBn = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubBranchUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubBranchUnits_SpecialBranchUnits_specialBranchUnitId",
                        column: x => x.specialBranchUnitId,
                        principalTable: "SpecialBranchUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spouses_districtId",
                table: "Spouses",
                column: "districtId");

            migrationBuilder.CreateIndex(
                name: "IX_SubBranchUnits_specialBranchUnitId",
                table: "SubBranchUnits",
                column: "specialBranchUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spouses_Districts_districtId",
                table: "Spouses",
                column: "districtId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spouses_Districts_districtId",
                table: "Spouses");

            migrationBuilder.DropTable(
                name: "SubBranchUnits");

            migrationBuilder.DropIndex(
                name: "IX_Spouses_districtId",
                table: "Spouses");

            migrationBuilder.DropColumn(
                name: "districtId",
                table: "Spouses");
        }
    }
}
