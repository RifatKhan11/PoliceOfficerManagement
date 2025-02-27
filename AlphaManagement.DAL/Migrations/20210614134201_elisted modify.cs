using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class elistedmodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rankId",
                table: "EnlistedAssignments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "refDate",
                table: "EnlistedAssignments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "refNo",
                table: "EnlistedAssignments",
                type: "NVARCHAR(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnlistedAssignments_rankId",
                table: "EnlistedAssignments",
                column: "rankId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnlistedAssignments_Ranks_rankId",
                table: "EnlistedAssignments",
                column: "rankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnlistedAssignments_Ranks_rankId",
                table: "EnlistedAssignments");

            migrationBuilder.DropIndex(
                name: "IX_EnlistedAssignments_rankId",
                table: "EnlistedAssignments");

            migrationBuilder.DropColumn(
                name: "rankId",
                table: "EnlistedAssignments");

            migrationBuilder.DropColumn(
                name: "refDate",
                table: "EnlistedAssignments");

            migrationBuilder.DropColumn(
                name: "refNo",
                table: "EnlistedAssignments");
        }
    }
}
