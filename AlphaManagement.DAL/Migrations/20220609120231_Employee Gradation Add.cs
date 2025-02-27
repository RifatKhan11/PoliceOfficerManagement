using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class EmployeeGradationAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeGradations",
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
                    employeeCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    nameEnglish = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    nameBangla = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    dateOfBirth = table.Column<DateTime>(nullable: true),
                    joiningDateGovtService = table.Column<DateTime>(nullable: true),
                    firstPromotionDate = table.Column<DateTime>(nullable: true),
                    lastPromotionDate = table.Column<DateTime>(nullable: true),
                    commentDate = table.Column<DateTime>(nullable: true),
                    LPRDate = table.Column<DateTime>(nullable: true),
                    joiningRankId = table.Column<int>(nullable: true),
                    rankId = table.Column<int>(nullable: true),
                    designationsId = table.Column<int>(nullable: true),
                    sectionId = table.Column<int>(nullable: true),
                    bCSBatchId = table.Column<int>(nullable: true),
                    bcsPosition = table.Column<int>(nullable: true),
                    experienceName = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    expReqDate = table.Column<DateTime>(nullable: true),
                    educationQualification = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    homeDistrict = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    imageUrl = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    commissionReceiptDate = table.Column<DateTime>(nullable: true),
                    commissionLPRDate = table.Column<DateTime>(nullable: true),
                    civilReqDate = table.Column<DateTime>(nullable: true),
                    civilRank = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    comments = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    firstAdhoc = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    secondAdhoc = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    thirdAdhoc = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    forthAdhoc = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    gradationSerial = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeGradations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeGradations_BCSBatches_bCSBatchId",
                        column: x => x.bCSBatchId,
                        principalTable: "BCSBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeGradations_Designations_designationsId",
                        column: x => x.designationsId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeGradations_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeGradations_Ranks_joiningRankId",
                        column: x => x.joiningRankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeGradations_Ranks_rankId",
                        column: x => x.rankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeGradations_Sections_sectionId",
                        column: x => x.sectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_bCSBatchId",
                table: "EmployeeGradations",
                column: "bCSBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_designationsId",
                table: "EmployeeGradations",
                column: "designationsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_employeeId",
                table: "EmployeeGradations",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_joiningRankId",
                table: "EmployeeGradations",
                column: "joiningRankId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_rankId",
                table: "EmployeeGradations",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGradations_sectionId",
                table: "EmployeeGradations",
                column: "sectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeGradations");
        }
    }
}
