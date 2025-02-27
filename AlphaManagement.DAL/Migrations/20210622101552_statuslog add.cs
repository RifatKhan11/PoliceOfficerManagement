using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class statuslogadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentStatusLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    assignmentId = table.Column<int>(nullable: true),
                    enlistedAssignmentId = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    statusInfoId = table.Column<int>(nullable: true),
                    employeeId = table.Column<int>(nullable: true),
                    empName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    nextEmpName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentStatusLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentStatusLogs_AssignmentMasters_assignmentId",
                        column: x => x.assignmentId,
                        principalTable: "AssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentStatusLogs_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentStatusLogs_EnlistedAssignmentMasters_enlistedAssignmentId",
                        column: x => x.enlistedAssignmentId,
                        principalTable: "EnlistedAssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentStatusLogs_StatusInfos_statusInfoId",
                        column: x => x.statusInfoId,
                        principalTable: "StatusInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentStatusLogs_assignmentId",
                table: "AssignmentStatusLogs",
                column: "assignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentStatusLogs_employeeId",
                table: "AssignmentStatusLogs",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentStatusLogs_enlistedAssignmentId",
                table: "AssignmentStatusLogs",
                column: "enlistedAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentStatusLogs_statusInfoId",
                table: "AssignmentStatusLogs",
                column: "statusInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentStatusLogs");
        }
    }
}
