using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class assignmentmasteradd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "specialBranchUnitId",
                table: "Sections",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assignmentMasterId",
                table: "Assignments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnulipiLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    copyName = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    copyNameBn = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnulipiLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentMasters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    refNo = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    refDate = table.Column<DateTime>(nullable: false),
                    applicationUserId = table.Column<string>(nullable: true),
                    statusId = table.Column<int>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentMasters_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentAnulipis",
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
                    anulipId = table.Column<int>(nullable: true),
                    anulipiId = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentAnulipis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentAnulipis_AnulipiLists_anulipiId",
                        column: x => x.anulipiId,
                        principalTable: "AnulipiLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentAnulipis_AssignmentMasters_assignmentMasterId",
                        column: x => x.assignmentMasterId,
                        principalTable: "AssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_specialBranchUnitId",
                table: "Sections",
                column: "specialBranchUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_assignmentMasterId",
                table: "Assignments",
                column: "assignmentMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAnulipis_anulipiId",
                table: "AssignmentAnulipis",
                column: "anulipiId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAnulipis_assignmentMasterId",
                table: "AssignmentAnulipis",
                column: "assignmentMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentMasters_applicationUserId",
                table: "AssignmentMasters",
                column: "applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AssignmentMasters_assignmentMasterId",
                table: "Assignments",
                column: "assignmentMasterId",
                principalTable: "AssignmentMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_SpecialBranchUnits_specialBranchUnitId",
                table: "Sections",
                column: "specialBranchUnitId",
                principalTable: "SpecialBranchUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AssignmentMasters_assignmentMasterId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_SpecialBranchUnits_specialBranchUnitId",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "AssignmentAnulipis");

            migrationBuilder.DropTable(
                name: "AnulipiLists");

            migrationBuilder.DropTable(
                name: "AssignmentMasters");

            migrationBuilder.DropIndex(
                name: "IX_Sections_specialBranchUnitId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_assignmentMasterId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "specialBranchUnitId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "assignmentMasterId",
                table: "Assignments");
        }
    }
}
