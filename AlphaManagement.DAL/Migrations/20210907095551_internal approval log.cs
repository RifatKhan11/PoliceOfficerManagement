using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class internalapprovallog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalApprovalLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    masterId = table.Column<int>(nullable: true),
                    MyProperty = table.Column<int>(nullable: false),
                    matrixTypeId = table.Column<int>(nullable: true),
                    userId = table.Column<string>(nullable: true),
                    nextApprovarId = table.Column<string>(nullable: true),
                    approverTypeId = table.Column<int>(nullable: true),
                    isActive = table.Column<int>(nullable: true),
                    sequenseNo = table.Column<int>(nullable: true),
                    notes = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalApprovalLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalApprovalLogs_ApproverTypes_approverTypeId",
                        column: x => x.approverTypeId,
                        principalTable: "ApproverTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalApprovalLogs_InternalEnlistedAssignmentMasters_masterId",
                        column: x => x.masterId,
                        principalTable: "InternalEnlistedAssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalApprovalLogs_MatrixTypes_matrixTypeId",
                        column: x => x.matrixTypeId,
                        principalTable: "MatrixTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalApprovalLogs_AspNetUsers_nextApprovarId",
                        column: x => x.nextApprovarId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalApprovalLogs_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalApprovalLogs_approverTypeId",
                table: "InternalApprovalLogs",
                column: "approverTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalApprovalLogs_masterId",
                table: "InternalApprovalLogs",
                column: "masterId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalApprovalLogs_matrixTypeId",
                table: "InternalApprovalLogs",
                column: "matrixTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalApprovalLogs_nextApprovarId",
                table: "InternalApprovalLogs",
                column: "nextApprovarId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalApprovalLogs_userId",
                table: "InternalApprovalLogs",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalApprovalLogs");
        }
    }
}
