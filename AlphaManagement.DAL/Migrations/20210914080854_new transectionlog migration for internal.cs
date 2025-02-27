using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class newtransectionlogmigrationforinternal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalAssignmentTransectionLogs",
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
                    table.PrimaryKey("PK_InternalAssignmentTransectionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentTransectionLogs_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentTransectionLogs_InternalEnlistedAssignments_assignmentId",
                        column: x => x.assignmentId,
                        principalTable: "InternalEnlistedAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentTransectionLogs_Departments_newSectionId",
                        column: x => x.newSectionId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentTransectionLogs_Departments_oldSectionId",
                        column: x => x.oldSectionId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentTransectionLogs_UpdateUserId",
                table: "InternalAssignmentTransectionLogs",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentTransectionLogs_assignmentId",
                table: "InternalAssignmentTransectionLogs",
                column: "assignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentTransectionLogs_newSectionId",
                table: "InternalAssignmentTransectionLogs",
                column: "newSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentTransectionLogs_oldSectionId",
                table: "InternalAssignmentTransectionLogs",
                column: "oldSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalAssignmentTransectionLogs");
        }
    }
}
