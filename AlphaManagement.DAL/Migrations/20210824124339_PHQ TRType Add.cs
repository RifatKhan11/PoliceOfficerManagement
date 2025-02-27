 using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class PHQTRTypeAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "attachmentBranchId",
                table: "EmployeeInfos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "countryId",
                table: "EmployeeInfos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pHQTRTypeId",
                table: "EmployeeInfos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PHQTRTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    trTypeName = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    trTypeNameBn = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHQTRTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfos_attachmentBranchId",
                table: "EmployeeInfos",
                column: "attachmentBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfos_countryId",
                table: "EmployeeInfos",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfos_pHQTRTypeId",
                table: "EmployeeInfos",
                column: "pHQTRTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInfos_SpecialBranchUnits_attachmentBranchId",
                table: "EmployeeInfos",
                column: "attachmentBranchId",
                principalTable: "SpecialBranchUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInfos_Countries_countryId",
                table: "EmployeeInfos",
                column: "countryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInfos_PHQTRTypes_pHQTRTypeId",
                table: "EmployeeInfos",
                column: "pHQTRTypeId",
                principalTable: "PHQTRTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInfos_SpecialBranchUnits_attachmentBranchId",
                table: "EmployeeInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInfos_Countries_countryId",
                table: "EmployeeInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInfos_PHQTRTypes_pHQTRTypeId",
                table: "EmployeeInfos");

            migrationBuilder.DropTable(
                name: "PHQTRTypes");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInfos_attachmentBranchId",
                table: "EmployeeInfos");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInfos_countryId",
                table: "EmployeeInfos");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInfos_pHQTRTypeId",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "attachmentBranchId",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "countryId",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "pHQTRTypeId",
                table: "EmployeeInfos");
        }
    }
}
