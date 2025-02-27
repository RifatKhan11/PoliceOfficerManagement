using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class signatureadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "noteSheetDescription",
                table: "AssignmentMasters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "noteSheetTitle",
                table: "AssignmentMasters",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssignmentAnulipiPreviews",
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
                    anulipiListId = table.Column<int>(nullable: true),
                    shortOrder = table.Column<int>(nullable: true),
                    anulipiText = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentAnulipiPreviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentAnulipiPreviews_AnulipiLists_anulipiListId",
                        column: x => x.anulipiListId,
                        principalTable: "AnulipiLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentAnulipiPreviews_AssignmentMasters_assignmentMasterId",
                        column: x => x.assignmentMasterId,
                        principalTable: "AssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentSignatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    igpSignId = table.Column<int>(nullable: true),
                    addlIGPSignId = table.Column<int>(nullable: true),
                    digSignId = table.Column<int>(nullable: true),
                    addlDIGSignId = table.Column<int>(nullable: true),
                    spSignId = table.Column<int>(nullable: true),
                    addlSPSignId = table.Column<int>(nullable: true),
                    aspSignSignId = table.Column<int>(nullable: true),
                    aspSignId = table.Column<int>(nullable: true),
                    inspectorSignId = table.Column<int>(nullable: true),
                    typeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSignatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentSignatures_EmployeeInfos_addlDIGSignId",
                        column: x => x.addlDIGSignId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentSignatures_EmployeeInfos_addlIGPSignId",
                        column: x => x.addlIGPSignId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentSignatures_EmployeeInfos_addlSPSignId",
                        column: x => x.addlSPSignId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentSignatures_EmployeeInfos_aspSignId",
                        column: x => x.aspSignId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentSignatures_EmployeeInfos_digSignId",
                        column: x => x.digSignId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentSignatures_EmployeeInfos_igpSignId",
                        column: x => x.igpSignId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentSignatures_EmployeeInfos_inspectorSignId",
                        column: x => x.inspectorSignId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentSignatures_EmployeeInfos_spSignId",
                        column: x => x.spSignId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAnulipiPreviews_anulipiListId",
                table: "AssignmentAnulipiPreviews",
                column: "anulipiListId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAnulipiPreviews_assignmentMasterId",
                table: "AssignmentAnulipiPreviews",
                column: "assignmentMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSignatures_addlDIGSignId",
                table: "AssignmentSignatures",
                column: "addlDIGSignId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSignatures_addlIGPSignId",
                table: "AssignmentSignatures",
                column: "addlIGPSignId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSignatures_addlSPSignId",
                table: "AssignmentSignatures",
                column: "addlSPSignId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSignatures_aspSignId",
                table: "AssignmentSignatures",
                column: "aspSignId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSignatures_digSignId",
                table: "AssignmentSignatures",
                column: "digSignId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSignatures_igpSignId",
                table: "AssignmentSignatures",
                column: "igpSignId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSignatures_inspectorSignId",
                table: "AssignmentSignatures",
                column: "inspectorSignId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSignatures_spSignId",
                table: "AssignmentSignatures",
                column: "spSignId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentAnulipiPreviews");

            migrationBuilder.DropTable(
                name: "AssignmentSignatures");

            migrationBuilder.DropColumn(
                name: "noteSheetDescription",
                table: "AssignmentMasters");

            migrationBuilder.DropColumn(
                name: "noteSheetTitle",
                table: "AssignmentMasters");
        }
    }
}
