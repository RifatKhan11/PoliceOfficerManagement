using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class Phonebookgp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneBookRanks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    name = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneBookRanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneBookUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    statusId = table.Column<int>(nullable: true),
                    parentId = table.Column<int>(nullable: true),
                    Priority = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneBookUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneBookUnits_PhoneBookUnits_parentId",
                        column: x => x.parentId,
                        principalTable: "PhoneBookUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    photo = table.Column<string>(nullable: true),
                    unitId = table.Column<int>(nullable: true),
                    rankId = table.Column<int>(nullable: true),
                    rank_name = table.Column<string>(nullable: true),
                    designation_name = table.Column<string>(nullable: true),
                    batch_bcs = table.Column<int>(nullable: true),
                    phone_office = table.Column<string>(nullable: true),
                    telephone = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneBooks_PhoneBookRanks_rankId",
                        column: x => x.rankId,
                        principalTable: "PhoneBookRanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhoneBooks_PhoneBookUnits_unitId",
                        column: x => x.unitId,
                        principalTable: "PhoneBookUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneBooks_rankId",
                table: "PhoneBooks",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneBooks_unitId",
                table: "PhoneBooks",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneBookUnits_parentId",
                table: "PhoneBookUnits",
                column: "parentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneBooks");

            migrationBuilder.DropTable(
                name: "PhoneBookRanks");

            migrationBuilder.DropTable(
                name: "PhoneBookUnits");
        }
    }
}
