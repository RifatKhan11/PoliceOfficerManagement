using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class employeeprinthistorylog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employeePrintHistoryLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    userId = table.Column<string>(maxLength: 250, nullable: true),
                    logTime = table.Column<DateTime>(maxLength: 250, nullable: false),
                    status = table.Column<int>(nullable: true),
                    ipAddress = table.Column<string>(maxLength: 250, nullable: true),
                    browserName = table.Column<string>(maxLength: 250, nullable: true),
                    pcName = table.Column<string>(maxLength: 250, nullable: true),
                    employeeInfosId = table.Column<int>(nullable: true),
                    printCode = table.Column<string>(nullable: true),
                    fileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeePrintHistoryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employeePrintHistoryLogs_EmployeeInfos_employeeInfosId",
                        column: x => x.employeeInfosId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employeePrintHistoryLogs_employeeInfosId",
                table: "employeePrintHistoryLogs",
                column: "employeeInfosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeePrintHistoryLogs");
        }
    }
}
