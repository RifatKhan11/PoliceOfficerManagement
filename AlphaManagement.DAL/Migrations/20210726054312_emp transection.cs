using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class emptransection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeTransectionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    employeeInfoId = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    statusInfoId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    empName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    nextEmpName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTransectionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeTransectionLogs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeTransectionLogs_EmployeeInfos_employeeInfoId",
                        column: x => x.employeeInfoId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeTransectionLogs_StatusInfos_statusInfoId",
                        column: x => x.statusInfoId,
                        principalTable: "StatusInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTransectionLogs_ApplicationUserId",
                table: "EmployeeTransectionLogs",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTransectionLogs_employeeInfoId",
                table: "EmployeeTransectionLogs",
                column: "employeeInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTransectionLogs_statusInfoId",
                table: "EmployeeTransectionLogs",
                column: "statusInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTransectionLogs");
        }
    }
}
