using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class Specialskilltableadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "specialSkillTypeId",
                table: "TraningLogs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "specialSkillTypes",
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
                    nameBn = table.Column<string>(nullable: true),
                    sortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialSkillTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TraningLogs_specialSkillTypeId",
                table: "TraningLogs",
                column: "specialSkillTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TraningLogs_specialSkillTypes_specialSkillTypeId",
                table: "TraningLogs",
                column: "specialSkillTypeId",
                principalTable: "specialSkillTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TraningLogs_specialSkillTypes_specialSkillTypeId",
                table: "TraningLogs");

            migrationBuilder.DropTable(
                name: "specialSkillTypes");

            migrationBuilder.DropIndex(
                name: "IX_TraningLogs_specialSkillTypeId",
                table: "TraningLogs");

            migrationBuilder.DropColumn(
                name: "specialSkillTypeId",
                table: "TraningLogs");
        }
    }
}
