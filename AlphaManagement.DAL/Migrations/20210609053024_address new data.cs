using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class addressnewdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DivisionInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    name = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    nameBn = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    url = table.Column<string>(type: "NVARCHAR(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UpazilaInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    name = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    nameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    url = table.Column<string>(type: "NVARCHAR(120)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpazilaInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistrictInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    divisionId = table.Column<int>(nullable: true),
                    name = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    nameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    url = table.Column<string>(type: "NVARCHAR(120)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistrictInfos_DivisionInfos_divisionId",
                        column: x => x.divisionId,
                        principalTable: "DivisionInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnionInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    districtId = table.Column<int>(nullable: true),
                    name = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    nameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    url = table.Column<string>(type: "NVARCHAR(120)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnionInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnionInfos_DistrictInfos_districtId",
                        column: x => x.districtId,
                        principalTable: "DistrictInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistrictInfos_divisionId",
                table: "DistrictInfos",
                column: "divisionId");

            migrationBuilder.CreateIndex(
                name: "IX_UnionInfos_districtId",
                table: "UnionInfos",
                column: "districtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnionInfos");

            migrationBuilder.DropTable(
                name: "UpazilaInfos");

            migrationBuilder.DropTable(
                name: "DistrictInfos");

            migrationBuilder.DropTable(
                name: "DivisionInfos");
        }
    }
}
