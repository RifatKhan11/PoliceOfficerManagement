using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class monjori : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostInUnits",
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
                    rankId = table.Column<int>(nullable: true),
                    numOfPost = table.Column<int>(nullable: true),
                    status = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostInUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostInUnits_Ranks_rankId",
                        column: x => x.rankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostInUnits_SpecialBranchUnits_specialBranchUnitId",
                        column: x => x.specialBranchUnitId,
                        principalTable: "SpecialBranchUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostInUnits_rankId",
                table: "PostInUnits",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_PostInUnits_specialBranchUnitId",
                table: "PostInUnits",
                column: "specialBranchUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostInUnits");
        }
    }
}
