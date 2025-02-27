using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class CancelAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CancelAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    assignmentMasterId = table.Column<int>(nullable: true),
                    assignmentId = table.Column<int>(nullable: true),
                    employeeId = table.Column<int>(nullable: true),
                    EntryNo = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    rankId = table.Column<int>(nullable: true),
                    specialBranchUnitId = table.Column<int>(nullable: true),
                    sectionId = table.Column<int>(nullable: true),
                    sectionName = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    servicePeriod = table.Column<string>(type: "NVARCHAR(250)", nullable: true),
                    Remarks = table.Column<string>(type: "NVARCHAR(550)", nullable: true),
                    designationName = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    unitName = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    statusId = table.Column<int>(nullable: true),
                    isAdminEntry = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelAssignments_Assignments_assignmentId",
                        column: x => x.assignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelAssignments_AssignmentMasters_assignmentMasterId",
                        column: x => x.assignmentMasterId,
                        principalTable: "AssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelAssignments_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelAssignments_Ranks_rankId",
                        column: x => x.rankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelAssignments_Sections_sectionId",
                        column: x => x.sectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelAssignments_SpecialBranchUnits_specialBranchUnitId",
                        column: x => x.specialBranchUnitId,
                        principalTable: "SpecialBranchUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancelAssignments_assignmentId",
                table: "CancelAssignments",
                column: "assignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelAssignments_assignmentMasterId",
                table: "CancelAssignments",
                column: "assignmentMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelAssignments_employeeId",
                table: "CancelAssignments",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelAssignments_rankId",
                table: "CancelAssignments",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelAssignments_sectionId",
                table: "CancelAssignments",
                column: "sectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelAssignments_specialBranchUnitId",
                table: "CancelAssignments",
                column: "specialBranchUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelAssignments");
        }
    }
}
