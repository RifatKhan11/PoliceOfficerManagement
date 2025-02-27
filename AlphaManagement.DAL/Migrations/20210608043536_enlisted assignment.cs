using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class enlistedassignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnlistedAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    specialBranchUnitId = table.Column<int>(nullable: true),
                    statusId = table.Column<int>(nullable: true),
                    typeId = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(type: "NVARCHAR(550)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnlistedAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnlistedAssignments_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnlistedAssignments_SpecialBranchUnits_specialBranchUnitId",
                        column: x => x.specialBranchUnitId,
                        principalTable: "SpecialBranchUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnlistedAssignments_employeeId",
                table: "EnlistedAssignments",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnlistedAssignments_specialBranchUnitId",
                table: "EnlistedAssignments",
                column: "specialBranchUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnlistedAssignments");
        }
    }
}
