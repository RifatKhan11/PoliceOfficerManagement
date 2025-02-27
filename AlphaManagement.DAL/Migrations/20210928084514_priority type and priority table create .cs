using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class prioritytypeandprioritytablecreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "priorityLevelTypes",
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
                    table.PrimaryKey("PK_priorityLevelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "postingPriorityLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    priorityLevelTypeId = table.Column<int>(nullable: true),
                    status = table.Column<int>(nullable: true),
                    prorityLevel = table.Column<string>(nullable: true),
                    sortOrder = table.Column<int>(nullable: true),
                    employeeInfoId = table.Column<int>(nullable: true),
                    rankId = table.Column<int>(nullable: true),
                    specialBranchUnitId = table.Column<int>(nullable: true),
                    specialSkillTypeId = table.Column<int>(nullable: true),
                    districtId = table.Column<int>(nullable: true),
                    ruleDescription = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postingPriorityLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_postingPriorityLevels_Districts_districtId",
                        column: x => x.districtId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_postingPriorityLevels_EmployeeInfos_employeeInfoId",
                        column: x => x.employeeInfoId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_postingPriorityLevels_priorityLevelTypes_priorityLevelTypeId",
                        column: x => x.priorityLevelTypeId,
                        principalTable: "priorityLevelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_postingPriorityLevels_Ranks_rankId",
                        column: x => x.rankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_postingPriorityLevels_SpecialBranchUnits_specialBranchUnitId",
                        column: x => x.specialBranchUnitId,
                        principalTable: "SpecialBranchUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_postingPriorityLevels_specialSkillTypes_specialSkillTypeId",
                        column: x => x.specialSkillTypeId,
                        principalTable: "specialSkillTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_postingPriorityLevels_districtId",
                table: "postingPriorityLevels",
                column: "districtId");

            migrationBuilder.CreateIndex(
                name: "IX_postingPriorityLevels_employeeInfoId",
                table: "postingPriorityLevels",
                column: "employeeInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_postingPriorityLevels_priorityLevelTypeId",
                table: "postingPriorityLevels",
                column: "priorityLevelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_postingPriorityLevels_rankId",
                table: "postingPriorityLevels",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_postingPriorityLevels_specialBranchUnitId",
                table: "postingPriorityLevels",
                column: "specialBranchUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_postingPriorityLevels_specialSkillTypeId",
                table: "postingPriorityLevels",
                column: "specialSkillTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "postingPriorityLevels");

            migrationBuilder.DropTable(
                name: "priorityLevelTypes");
        }
    }
}
