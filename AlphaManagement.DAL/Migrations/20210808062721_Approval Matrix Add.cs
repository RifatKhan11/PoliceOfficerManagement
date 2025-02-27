using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class ApprovalMatrixAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApproverTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    approverTypeName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    approverTypeNameBn = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatrixTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    matrixTypeName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    matrixTypeNameBn = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrixTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalLogs",
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
                    table.PrimaryKey("PK_ApprovalLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalLogs_ApproverTypes_approverTypeId",
                        column: x => x.approverTypeId,
                        principalTable: "ApproverTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalLogs_AssignmentMasters_masterId",
                        column: x => x.masterId,
                        principalTable: "AssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalLogs_MatrixTypes_matrixTypeId",
                        column: x => x.matrixTypeId,
                        principalTable: "MatrixTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalLogs_AspNetUsers_nextApprovarId",
                        column: x => x.nextApprovarId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalLogs_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalMatrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    matrixTypeId = table.Column<int>(nullable: true),
                    userId = table.Column<string>(nullable: true),
                    nextApprovarId = table.Column<string>(nullable: true),
                    approverTypeId = table.Column<int>(nullable: true),
                    isActive = table.Column<int>(nullable: true),
                    sequenseNo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalMatrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalMatrices_ApproverTypes_approverTypeId",
                        column: x => x.approverTypeId,
                        principalTable: "ApproverTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalMatrices_MatrixTypes_matrixTypeId",
                        column: x => x.matrixTypeId,
                        principalTable: "MatrixTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalMatrices_AspNetUsers_nextApprovarId",
                        column: x => x.nextApprovarId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalMatrices_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLogs_approverTypeId",
                table: "ApprovalLogs",
                column: "approverTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLogs_masterId",
                table: "ApprovalLogs",
                column: "masterId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLogs_matrixTypeId",
                table: "ApprovalLogs",
                column: "matrixTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLogs_nextApprovarId",
                table: "ApprovalLogs",
                column: "nextApprovarId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLogs_userId",
                table: "ApprovalLogs",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalMatrices_approverTypeId",
                table: "ApprovalMatrices",
                column: "approverTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalMatrices_matrixTypeId",
                table: "ApprovalMatrices",
                column: "matrixTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalMatrices_nextApprovarId",
                table: "ApprovalMatrices",
                column: "nextApprovarId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalMatrices_userId",
                table: "ApprovalMatrices",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalLogs");

            migrationBuilder.DropTable(
                name: "ApprovalMatrices");

            migrationBuilder.DropTable(
                name: "ApproverTypes");

            migrationBuilder.DropTable(
                name: "MatrixTypes");
        }
    }
}
