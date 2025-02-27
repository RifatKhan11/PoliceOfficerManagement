using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class bcsbatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bCSBatchId",
                table: "EmployeeInfos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BCSBatches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    batchName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    batchNameBn = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BCSBatches", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfos_bCSBatchId",
                table: "EmployeeInfos",
                column: "bCSBatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInfos_BCSBatches_bCSBatchId",
                table: "EmployeeInfos",
                column: "bCSBatchId",
                principalTable: "BCSBatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInfos_BCSBatches_bCSBatchId",
                table: "EmployeeInfos");

            migrationBuilder.DropTable(
                name: "BCSBatches");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInfos_bCSBatchId",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "bCSBatchId",
                table: "EmployeeInfos");
        }
    }
}
