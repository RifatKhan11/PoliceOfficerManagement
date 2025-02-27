using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class internalpostingadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalAnulipiLists",
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
                    table.PrimaryKey("PK_InternalAnulipiLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternalAssignmentMasters",
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
                    description = table.Column<string>(nullable: true),
                    memorandumNo = table.Column<string>(type: "NVARCHAR(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalAssignmentMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentMasters_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InternalEnlistedAssignmentMasters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    refNo = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    refDate = table.Column<DateTime>(nullable: true),
                    statusId = table.Column<int>(nullable: true),
                    typeId = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(type: "NVARCHAR(550)", nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalEnlistedAssignmentMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalEnlistedAssignmentMasters_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InternalAssignmentAnulipis",
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
                    table.PrimaryKey("PK_InternalAssignmentAnulipis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentAnulipis_InternalAnulipiLists_anulipiListId",
                        column: x => x.anulipiListId,
                        principalTable: "InternalAnulipiLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentAnulipis_InternalAssignmentMasters_assignmentMasterId",
                        column: x => x.assignmentMasterId,
                        principalTable: "InternalAssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InternalAssignments",
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
                    employeeId = table.Column<int>(nullable: false),
                    assignmentTypeId = table.Column<int>(nullable: true),
                    EntryNo = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    rankId = table.Column<int>(nullable: true),
                    departmentId = table.Column<int>(nullable: true),
                    supervisorId = table.Column<int>(nullable: true),
                    reasonOfTransfer = table.Column<string>(nullable: true),
                    ministryRefNo = table.Column<string>(nullable: true),
                    receiveRefNo = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_InternalAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalAssignments_InternalAssignmentMasters_assignmentMasterId",
                        column: x => x.assignmentMasterId,
                        principalTable: "InternalAssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignments_Departments_departmentId",
                        column: x => x.departmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignments_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalAssignments_Ranks_rankId",
                        column: x => x.rankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignments_Sections_sectionId",
                        column: x => x.sectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignments_EmployeeInfos_supervisorId",
                        column: x => x.supervisorId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InternalAssignmentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    assignmentId = table.Column<int>(nullable: true),
                    enlistedAssignmentId = table.Column<int>(nullable: true),
                    applicationUserId = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    statusInfoId = table.Column<int>(nullable: true),
                    employeeId = table.Column<int>(nullable: true),
                    empName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    nextEmpName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalAssignmentStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentStatuses_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentStatuses_InternalAssignmentMasters_assignmentId",
                        column: x => x.assignmentId,
                        principalTable: "InternalAssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentStatuses_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentStatuses_InternalEnlistedAssignmentMasters_enlistedAssignmentId",
                        column: x => x.enlistedAssignmentId,
                        principalTable: "InternalEnlistedAssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalAssignmentStatuses_StatusInfos_statusInfoId",
                        column: x => x.statusInfoId,
                        principalTable: "StatusInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InternalEnlistedAssignments",
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
                    rankId = table.Column<int>(nullable: true),
                    statusId = table.Column<int>(nullable: true),
                    typeId = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(type: "NVARCHAR(550)", nullable: true),
                    enlistedAssignmentMasterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalEnlistedAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalEnlistedAssignments_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalEnlistedAssignments_InternalEnlistedAssignmentMasters_enlistedAssignmentMasterId",
                        column: x => x.enlistedAssignmentMasterId,
                        principalTable: "InternalEnlistedAssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalEnlistedAssignments_Ranks_rankId",
                        column: x => x.rankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalEnlistedAssignments_SpecialBranchUnits_specialBranchUnitId",
                        column: x => x.specialBranchUnitId,
                        principalTable: "SpecialBranchUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InternalCancelAssignments",
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
                    table.PrimaryKey("PK_InternalCancelAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalCancelAssignments_InternalAssignments_assignmentId",
                        column: x => x.assignmentId,
                        principalTable: "InternalAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalCancelAssignments_InternalAssignmentMasters_assignmentMasterId",
                        column: x => x.assignmentMasterId,
                        principalTable: "InternalAssignmentMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalCancelAssignments_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalCancelAssignments_Ranks_rankId",
                        column: x => x.rankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalCancelAssignments_Sections_sectionId",
                        column: x => x.sectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalCancelAssignments_SpecialBranchUnits_specialBranchUnitId",
                        column: x => x.specialBranchUnitId,
                        principalTable: "SpecialBranchUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentAnulipis_anulipiListId",
                table: "InternalAssignmentAnulipis",
                column: "anulipiListId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentAnulipis_assignmentMasterId",
                table: "InternalAssignmentAnulipis",
                column: "assignmentMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentMasters_applicationUserId",
                table: "InternalAssignmentMasters",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignments_assignmentMasterId",
                table: "InternalAssignments",
                column: "assignmentMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignments_departmentId",
                table: "InternalAssignments",
                column: "departmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignments_employeeId",
                table: "InternalAssignments",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignments_rankId",
                table: "InternalAssignments",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignments_sectionId",
                table: "InternalAssignments",
                column: "sectionId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignments_supervisorId",
                table: "InternalAssignments",
                column: "supervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentStatuses_applicationUserId",
                table: "InternalAssignmentStatuses",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentStatuses_assignmentId",
                table: "InternalAssignmentStatuses",
                column: "assignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentStatuses_employeeId",
                table: "InternalAssignmentStatuses",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentStatuses_enlistedAssignmentId",
                table: "InternalAssignmentStatuses",
                column: "enlistedAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignmentStatuses_statusInfoId",
                table: "InternalAssignmentStatuses",
                column: "statusInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalCancelAssignments_assignmentId",
                table: "InternalCancelAssignments",
                column: "assignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalCancelAssignments_assignmentMasterId",
                table: "InternalCancelAssignments",
                column: "assignmentMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalCancelAssignments_employeeId",
                table: "InternalCancelAssignments",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalCancelAssignments_rankId",
                table: "InternalCancelAssignments",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalCancelAssignments_sectionId",
                table: "InternalCancelAssignments",
                column: "sectionId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalCancelAssignments_specialBranchUnitId",
                table: "InternalCancelAssignments",
                column: "specialBranchUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalEnlistedAssignmentMasters_ApplicationUserId",
                table: "InternalEnlistedAssignmentMasters",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalEnlistedAssignments_employeeId",
                table: "InternalEnlistedAssignments",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalEnlistedAssignments_enlistedAssignmentMasterId",
                table: "InternalEnlistedAssignments",
                column: "enlistedAssignmentMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalEnlistedAssignments_rankId",
                table: "InternalEnlistedAssignments",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalEnlistedAssignments_specialBranchUnitId",
                table: "InternalEnlistedAssignments",
                column: "specialBranchUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalAssignmentAnulipis");

            migrationBuilder.DropTable(
                name: "InternalAssignmentStatuses");

            migrationBuilder.DropTable(
                name: "InternalCancelAssignments");

            migrationBuilder.DropTable(
                name: "InternalEnlistedAssignments");

            migrationBuilder.DropTable(
                name: "InternalAnulipiLists");

            migrationBuilder.DropTable(
                name: "InternalAssignments");

            migrationBuilder.DropTable(
                name: "InternalEnlistedAssignmentMasters");

            migrationBuilder.DropTable(
                name: "InternalAssignmentMasters");
        }
    }
}
