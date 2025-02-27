using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class enrollist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "refDate",
                table: "EnlistedAssignments");

            migrationBuilder.DropColumn(
                name: "refNo",
                table: "EnlistedAssignments");

            migrationBuilder.AddColumn<int>(
                name: "enlistedAssignmentMasterId",
                table: "EnlistedAssignments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EnlistedAssignmentMasters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    refNo = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    refDate = table.Column<DateTime>(nullable: true),
                    statusId = table.Column<int>(nullable: true),
                    typeId = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(type: "NVARCHAR(550)", nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnlistedAssignmentMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnlistedAssignmentMasters_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnlistedAssignments_enlistedAssignmentMasterId",
                table: "EnlistedAssignments",
                column: "enlistedAssignmentMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_EnlistedAssignmentMasters_ApplicationUserId",
                table: "EnlistedAssignmentMasters",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnlistedAssignments_EnlistedAssignmentMasters_enlistedAssignmentMasterId",
                table: "EnlistedAssignments",
                column: "enlistedAssignmentMasterId",
                principalTable: "EnlistedAssignmentMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnlistedAssignments_EnlistedAssignmentMasters_enlistedAssignmentMasterId",
                table: "EnlistedAssignments");

            migrationBuilder.DropTable(
                name: "EnlistedAssignmentMasters");

            migrationBuilder.DropIndex(
                name: "IX_EnlistedAssignments_enlistedAssignmentMasterId",
                table: "EnlistedAssignments");

            migrationBuilder.DropColumn(
                name: "enlistedAssignmentMasterId",
                table: "EnlistedAssignments");

            migrationBuilder.AddColumn<DateTime>(
                name: "refDate",
                table: "EnlistedAssignments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "refNo",
                table: "EnlistedAssignments",
                type: "NVARCHAR(100)",
                nullable: true);
        }
    }
}
