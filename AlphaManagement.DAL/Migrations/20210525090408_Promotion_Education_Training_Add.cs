using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class Promotion_Education_Training_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignments",
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
                    assignmentTypeId = table.Column<int>(nullable: true),
                    EntryNo = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    designationId = table.Column<int>(nullable: true),
                    departmentId = table.Column<int>(nullable: true),
                    policeUnitId = table.Column<int>(nullable: true),
                    policeSubUnitId = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Departments_departmentId",
                        column: x => x.departmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignments_Designations_designationId",
                        column: x => x.designationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignments_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_PoliceSubUnits_policeSubUnitId",
                        column: x => x.policeSubUnitId,
                        principalTable: "PoliceSubUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignments_PoliceUnits_policeUnitId",
                        column: x => x.policeUnitId,
                        principalTable: "PoliceUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwardEntries",
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
                    awardName = table.Column<string>(type: "NVARCHAR(250)", nullable: true),
                    purpose = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    awardDate = table.Column<DateTime>(nullable: false),
                    status = table.Column<string>(type: "NVARCHAR(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardEntries_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    awardName = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    awardNameBn = table.Column<string>(type: "NVARCHAR(250)", nullable: true),
                    awardShortName = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NaturalPunishments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    name = table.Column<string>(type: "NVARCHAR(250)", nullable: true),
                    description = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalPunishments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    offense = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    description = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    organizationName = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    organizationNameBn = table.Column<string>(type: "NVARCHAR(250)", nullable: true),
                    organizationType = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtherQualificationHeads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    name = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherQualificationHeads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryGrades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    gradeName = table.Column<string>(maxLength: 100, nullable: true),
                    basicAmount = table.Column<decimal>(nullable: true),
                    payScale = table.Column<string>(maxLength: 100, nullable: true),
                    amount = table.Column<decimal>(nullable: true),
                    type = table.Column<string>(maxLength: 100, nullable: true),
                    currentBasic = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryGrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TraningLogs",
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
                    fromDate = table.Column<DateTime>(nullable: true),
                    toDate = table.Column<DateTime>(nullable: true),
                    countryId = table.Column<int>(nullable: true),
                    trainingCategoryId = table.Column<int>(nullable: true),
                    trainingInstituteId = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    trainingTitle = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    sponsoringAgency = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraningLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraningLogs_Countries_countryId",
                        column: x => x.countryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraningLogs_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraningLogs_TrainingCategories_trainingCategoryId",
                        column: x => x.trainingCategoryId,
                        principalTable: "TrainingCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraningLogs_TrainingInstitutes_trainingInstituteId",
                        column: x => x.trainingInstituteId,
                        principalTable: "TrainingInstitutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaryActions",
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
                    OffenseId = table.Column<int>(nullable: true),
                    naturalPunishmentId = table.Column<int>(nullable: true),
                    punishmentDate = table.Column<DateTime>(nullable: true),
                    startingDate = table.Column<DateTime>(nullable: true),
                    endDate = table.Column<DateTime>(nullable: true),
                    goNumberWithDate = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    goFileURL = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    status = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaryActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplinaryActions_Offenses_OffenseId",
                        column: x => x.OffenseId,
                        principalTable: "Offenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DisciplinaryActions_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplinaryActions_NaturalPunishments_naturalPunishmentId",
                        column: x => x.naturalPunishmentId,
                        principalTable: "NaturalPunishments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducationalQualifications",
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
                    institution = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    resultId = table.Column<int>(nullable: true),
                    majorGroup = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    grade = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    passingYear = table.Column<int>(nullable: true),
                    degreeId = table.Column<int>(nullable: true),
                    organizationId = table.Column<int>(nullable: true),
                    reldegreesubjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationalQualifications_Degrees_degreeId",
                        column: x => x.degreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationalQualifications_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationalQualifications_Organizations_organizationId",
                        column: x => x.organizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationalQualifications_RelDegreeSubjects_reldegreesubjectId",
                        column: x => x.reldegreesubjectId,
                        principalTable: "RelDegreeSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationalQualifications_Results_resultId",
                        column: x => x.resultId,
                        principalTable: "Results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PromotionLogs",
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
                    designation = table.Column<string>(nullable: true),
                    designationNewId = table.Column<int>(nullable: true),
                    designationOldId = table.Column<int>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    payScaleId = table.Column<int>(nullable: true),
                    goNumber = table.Column<string>(nullable: true),
                    goDate = table.Column<DateTime>(nullable: true),
                    remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionLogs_Designations_designationNewId",
                        column: x => x.designationNewId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionLogs_Designations_designationOldId",
                        column: x => x.designationOldId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionLogs_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionLogs_SalaryGrades_payScaleId",
                        column: x => x.payScaleId,
                        principalTable: "SalaryGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_departmentId",
                table: "Assignments",
                column: "departmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_designationId",
                table: "Assignments",
                column: "designationId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_employeeId",
                table: "Assignments",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_policeSubUnitId",
                table: "Assignments",
                column: "policeSubUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_policeUnitId",
                table: "Assignments",
                column: "policeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardEntries_employeeId",
                table: "AwardEntries",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaryActions_OffenseId",
                table: "DisciplinaryActions",
                column: "OffenseId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaryActions_employeeId",
                table: "DisciplinaryActions",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaryActions_naturalPunishmentId",
                table: "DisciplinaryActions",
                column: "naturalPunishmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualifications_degreeId",
                table: "EducationalQualifications",
                column: "degreeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualifications_employeeId",
                table: "EducationalQualifications",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualifications_organizationId",
                table: "EducationalQualifications",
                column: "organizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualifications_reldegreesubjectId",
                table: "EducationalQualifications",
                column: "reldegreesubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualifications_resultId",
                table: "EducationalQualifications",
                column: "resultId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogs_designationNewId",
                table: "PromotionLogs",
                column: "designationNewId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogs_designationOldId",
                table: "PromotionLogs",
                column: "designationOldId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogs_employeeId",
                table: "PromotionLogs",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogs_payScaleId",
                table: "PromotionLogs",
                column: "payScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_TraningLogs_countryId",
                table: "TraningLogs",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_TraningLogs_employeeId",
                table: "TraningLogs",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TraningLogs_trainingCategoryId",
                table: "TraningLogs",
                column: "trainingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TraningLogs_trainingInstituteId",
                table: "TraningLogs",
                column: "trainingInstituteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "AwardEntries");

            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropTable(
                name: "DisciplinaryActions");

            migrationBuilder.DropTable(
                name: "EducationalQualifications");

            migrationBuilder.DropTable(
                name: "OtherQualificationHeads");

            migrationBuilder.DropTable(
                name: "PromotionLogs");

            migrationBuilder.DropTable(
                name: "TraningLogs");

            migrationBuilder.DropTable(
                name: "Offenses");

            migrationBuilder.DropTable(
                name: "NaturalPunishments");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "SalaryGrades");
        }
    }
}
