using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class addtableAssignmentTransectionLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentTransectionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    assignmentId = table.Column<int>(nullable: true),
                    oldSectionId = table.Column<int>(nullable: true),
                    newSectionId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentTransectionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentTransectionLogs_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentTransectionLogs_Assignments_assignmentId",
                        column: x => x.assignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentTransectionLogs_Sections_newSectionId",
                        column: x => x.newSectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentTransectionLogs_Sections_oldSectionId",
                        column: x => x.oldSectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTransectionLogs_UpdateUserId",
                table: "AssignmentTransectionLogs",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTransectionLogs_assignmentId",
                table: "AssignmentTransectionLogs",
                column: "assignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTransectionLogs_newSectionId",
                table: "AssignmentTransectionLogs",
                column: "newSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTransectionLogs_oldSectionId",
                table: "AssignmentTransectionLogs",
                column: "oldSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentTransectionLogs");
        }
    }
}
