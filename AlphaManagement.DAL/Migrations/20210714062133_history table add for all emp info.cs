using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class historytableaddforallempinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeInfoHistoryId",
                table: "Photographs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeInfoHistoryId",
                table: "AddressInformation",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddressInformationHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    entryType = table.Column<int>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    addressInformationId = table.Column<int>(nullable: true),
                    employeeInfoId = table.Column<int>(nullable: true),
                    spouseId = table.Column<int>(nullable: true),
                    countryId = table.Column<int>(nullable: true),
                    divisionId = table.Column<int>(nullable: true),
                    districtId = table.Column<int>(nullable: true),
                    thanaId = table.Column<int>(nullable: true),
                    unionWardId = table.Column<int>(nullable: true),
                    villageId = table.Column<int>(nullable: true),
                    union = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    postOffice = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    postCode = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    blockSector = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    houseVillage = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    roadNumber = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    addressDetails = table.Column<string>(type: "NVARCHAR(250)", nullable: true),
                    oneLineAddress = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    type = table.Column<string>(type: "NVARCHAR(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressInformationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressInformationHistories_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressInformationHistories_AddressInformation_addressInformationId",
                        column: x => x.addressInformationId,
                        principalTable: "AddressInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressInformationHistories_Countries_countryId",
                        column: x => x.countryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressInformationHistories_Districts_districtId",
                        column: x => x.districtId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressInformationHistories_Divisions_divisionId",
                        column: x => x.divisionId,
                        principalTable: "Divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressInformationHistories_EmployeeInfos_employeeInfoId",
                        column: x => x.employeeInfoId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressInformationHistories_Spouses_spouseId",
                        column: x => x.spouseId,
                        principalTable: "Spouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressInformationHistories_Thanas_thanaId",
                        column: x => x.thanaId,
                        principalTable: "Thanas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressInformationHistories_UnionWards_unionWardId",
                        column: x => x.unionWardId,
                        principalTable: "UnionWards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressInformationHistories_Villages_villageId",
                        column: x => x.villageId,
                        principalTable: "Villages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    entryType = table.Column<int>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    assignmentId = table.Column<int>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    assignmentTypeId = table.Column<int>(nullable: true),
                    EntryNo = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    rankId = table.Column<int>(nullable: true),
                    designationId = table.Column<int>(nullable: true),
                    departmentId = table.Column<int>(nullable: true),
                    specialBranchUnitId = table.Column<int>(nullable: true),
                    sectionId = table.Column<int>(nullable: true),
                    sectionName = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    servicePeriod = table.Column<string>(type: "NVARCHAR(250)", nullable: true),
                    Remarks = table.Column<string>(type: "NVARCHAR(550)", nullable: true),
                    statusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentHistories_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentHistories_Assignments_assignmentId",
                        column: x => x.assignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentHistories_Departments_departmentId",
                        column: x => x.departmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentHistories_Designations_designationId",
                        column: x => x.designationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentHistories_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentHistories_Ranks_rankId",
                        column: x => x.rankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentHistories_Sections_sectionId",
                        column: x => x.sectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentHistories_SpecialBranchUnits_specialBranchUnitId",
                        column: x => x.specialBranchUnitId,
                        principalTable: "SpecialBranchUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwardEntryHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    entryType = table.Column<int>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    awardEntryId = table.Column<int>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    awardName = table.Column<string>(type: "NVARCHAR(250)", nullable: true),
                    awardId = table.Column<int>(nullable: true),
                    purpose = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    referenceNumber = table.Column<string>(nullable: true),
                    awardDate = table.Column<DateTime>(nullable: false),
                    status = table.Column<string>(type: "NVARCHAR(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardEntryHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardEntryHistories_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwardEntryHistories_AwardEntries_awardEntryId",
                        column: x => x.awardEntryId,
                        principalTable: "AwardEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwardEntryHistories_Awards_awardId",
                        column: x => x.awardId,
                        principalTable: "Awards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwardEntryHistories_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationalQualificationHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    entryType = table.Column<int>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    educationalQualificationId = table.Column<int>(nullable: true),
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
                    table.PrimaryKey("PK_EducationalQualificationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationalQualificationHistories_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationalQualificationHistories_Degrees_degreeId",
                        column: x => x.degreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationalQualificationHistories_EducationalQualifications_educationalQualificationId",
                        column: x => x.educationalQualificationId,
                        principalTable: "EducationalQualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationalQualificationHistories_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationalQualificationHistories_Organizations_organizationId",
                        column: x => x.organizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationalQualificationHistories_RelDegreeSubjects_reldegreesubjectId",
                        column: x => x.reldegreesubjectId,
                        principalTable: "RelDegreeSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationalQualificationHistories_Results_resultId",
                        column: x => x.resultId,
                        principalTable: "Results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeInfoHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    entryType = table.Column<int>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    employeeCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    nationalID = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    birthIdentificationNo = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    govtID = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    gpfNomineeName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    gpfAcNo = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    nameEnglish = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    nameBangla = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    motherNameEnglish = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    motherNameBangla = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    fatherNameEnglish = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    fatherNameBangla = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    nationality = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    disability = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    servicePeriod = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    salaryAccountNo = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    bankAccount = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    otherBankAccountNo = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    passportNo = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    facebookId = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    linkdInId = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    tribal = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    identificationSign = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    height = table.Column<decimal>(nullable: true),
                    weight = table.Column<decimal>(nullable: true),
                    dateOfBirth = table.Column<DateTime>(nullable: true),
                    joiningDatePresentWorkstation = table.Column<DateTime>(nullable: true),
                    joiningDateGovtService = table.Column<DateTime>(nullable: true),
                    dateofregularity = table.Column<DateTime>(nullable: true),
                    dateOfPermanent = table.Column<DateTime>(nullable: true),
                    LPRDate = table.Column<DateTime>(nullable: true),
                    PRLStartDate = table.Column<DateTime>(nullable: true),
                    PRLEndDate = table.Column<DateTime>(nullable: true),
                    promotionDate = table.Column<DateTime>(nullable: true),
                    gender = table.Column<string>(maxLength: 10, nullable: true),
                    birthPlace = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    maritalStatus = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    religionId = table.Column<int>(nullable: true),
                    employeeTypeId = table.Column<int>(nullable: true),
                    activityStatus = table.Column<int>(nullable: true),
                    departmentId = table.Column<int>(nullable: true),
                    batch = table.Column<string>(nullable: true),
                    bloodGroup = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    freedomFighter = table.Column<bool>(nullable: false),
                    freedomFighterNo = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    telephoneOffice = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    telephoneResidence = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    pabx = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    emailAddress = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    emailAddressPersonal = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    mobileNumberOffice = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    mobileNumberPersonal = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    specialSkill = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    seniorityNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    designation = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    skypeId = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    post = table.Column<int>(nullable: true),
                    designationCheck = table.Column<int>(nullable: false),
                    joiningDesignation = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    natureOfRequitment = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    homeDistrict = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    drivingLicense = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    rationId = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    branchId = table.Column<int>(nullable: true),
                    banksId = table.Column<int>(nullable: true),
                    otherBanksId = table.Column<int>(nullable: true),
                    rankId = table.Column<int>(nullable: true),
                    designationsId = table.Column<int>(nullable: true),
                    sectionId = table.Column<int>(nullable: true),
                    bCSBatchId = table.Column<int>(nullable: true),
                    bcsPosition = table.Column<int>(nullable: true),
                    departmentalPromotionYear = table.Column<int>(nullable: true),
                    skill = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    extraActivity = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    sectionName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    extraSkill = table.Column<string>(nullable: true),
                    extraActivitys = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    isApproved = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeInfoHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_BCSBatches_bCSBatchId",
                        column: x => x.bCSBatchId,
                        principalTable: "BCSBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_Banks_banksId",
                        column: x => x.banksId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_SpecialBranchUnits_branchId",
                        column: x => x.branchId,
                        principalTable: "SpecialBranchUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_Departments_departmentId",
                        column: x => x.departmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_Designations_designationsId",
                        column: x => x.designationsId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_EmployeeTypes_employeeTypeId",
                        column: x => x.employeeTypeId,
                        principalTable: "EmployeeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_Banks_otherBanksId",
                        column: x => x.otherBanksId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_Ranks_rankId",
                        column: x => x.rankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_Religions_religionId",
                        column: x => x.religionId,
                        principalTable: "Religions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoHistories_Sections_sectionId",
                        column: x => x.sectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ForeignTravelHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    entryType = table.Column<int>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    foreignTravelId = table.Column<int>(nullable: true),
                    employeeId = table.Column<int>(nullable: true),
                    countryId = table.Column<int>(nullable: true),
                    travelPurpose = table.Column<string>(nullable: true),
                    travelDate = table.Column<DateTime>(nullable: true),
                    travelEndDate = table.Column<DateTime>(nullable: true),
                    status = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    travelDuration = table.Column<string>(nullable: true),
                    referenceNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignTravelHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForeignTravelHistories_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ForeignTravelHistories_Countries_countryId",
                        column: x => x.countryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ForeignTravelHistories_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ForeignTravelHistories_ForeignTravels_foreignTravelId",
                        column: x => x.foreignTravelId,
                        principalTable: "ForeignTravels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PromotionLogHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    entryType = table.Column<int>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    promotionLogId = table.Column<int>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    designation = table.Column<string>(nullable: true),
                    designationNewId = table.Column<int>(nullable: true),
                    designationOldId = table.Column<int>(nullable: true),
                    rankId = table.Column<int>(nullable: true),
                    rankOldId = table.Column<int>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    payScaleId = table.Column<int>(nullable: true),
                    goNumber = table.Column<string>(nullable: true),
                    goDate = table.Column<DateTime>(nullable: true),
                    remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionLogHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionLogHistories_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionLogHistories_Designations_designationNewId",
                        column: x => x.designationNewId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionLogHistories_Designations_designationOldId",
                        column: x => x.designationOldId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionLogHistories_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionLogHistories_SalaryGrades_payScaleId",
                        column: x => x.payScaleId,
                        principalTable: "SalaryGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionLogHistories_PromotionLogs_promotionLogId",
                        column: x => x.promotionLogId,
                        principalTable: "PromotionLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionLogHistories_Ranks_rankId",
                        column: x => x.rankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionLogHistories_Ranks_rankOldId",
                        column: x => x.rankOldId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpouseHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    entryType = table.Column<int>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    spouseId = table.Column<int>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    spouseRelationId = table.Column<int>(nullable: true),
                    spouseName = table.Column<string>(maxLength: 250, nullable: true),
                    email = table.Column<string>(maxLength: 150, nullable: true),
                    spouseNameBN = table.Column<string>(maxLength: 250, nullable: true),
                    dateOfBirth = table.Column<DateTime>(nullable: true),
                    occupation = table.Column<string>(nullable: true),
                    gender = table.Column<string>(maxLength: 20, nullable: true),
                    designation = table.Column<string>(maxLength: 250, nullable: true),
                    organization = table.Column<string>(maxLength: 450, nullable: true),
                    bin = table.Column<string>(maxLength: 100, nullable: true),
                    nid = table.Column<string>(maxLength: 100, nullable: true),
                    bloodGroup = table.Column<string>(maxLength: 30, nullable: true),
                    contact = table.Column<string>(maxLength: 250, nullable: true),
                    highestEducation = table.Column<string>(maxLength: 450, nullable: true),
                    homeDistrict = table.Column<string>(maxLength: 100, nullable: true),
                    fatherName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    motherName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    maritalStatus = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    birthCertificate = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    districtId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpouseHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpouseHistories_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpouseHistories_Districts_districtId",
                        column: x => x.districtId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpouseHistories_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpouseHistories_Spouses_spouseId",
                        column: x => x.spouseId,
                        principalTable: "Spouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpouseHistories_SpouseRelations_spouseRelationId",
                        column: x => x.spouseRelationId,
                        principalTable: "SpouseRelations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TraningLogHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    entryType = table.Column<int>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    traningLogId = table.Column<int>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    fromDate = table.Column<DateTime>(nullable: true),
                    toDate = table.Column<DateTime>(nullable: true),
                    countryId = table.Column<int>(nullable: true),
                    trainingCategoryId = table.Column<int>(nullable: true),
                    trainingInstituteId = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    trainingTitle = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    sponsoringAgency = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    trainingType = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    referenceNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraningLogHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraningLogHistories_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraningLogHistories_Countries_countryId",
                        column: x => x.countryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraningLogHistories_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraningLogHistories_TrainingCategories_trainingCategoryId",
                        column: x => x.trainingCategoryId,
                        principalTable: "TrainingCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraningLogHistories_TrainingInstitutes_trainingInstituteId",
                        column: x => x.trainingInstituteId,
                        principalTable: "TrainingInstitutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraningLogHistories_TraningLogs_traningLogId",
                        column: x => x.traningLogId,
                        principalTable: "TraningLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photographs_EmployeeInfoHistoryId",
                table: "Photographs",
                column: "EmployeeInfoHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformation_EmployeeInfoHistoryId",
                table: "AddressInformation",
                column: "EmployeeInfoHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformationHistories_UpdateUserId",
                table: "AddressInformationHistories",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformationHistories_addressInformationId",
                table: "AddressInformationHistories",
                column: "addressInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformationHistories_countryId",
                table: "AddressInformationHistories",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformationHistories_districtId",
                table: "AddressInformationHistories",
                column: "districtId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformationHistories_divisionId",
                table: "AddressInformationHistories",
                column: "divisionId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformationHistories_employeeInfoId",
                table: "AddressInformationHistories",
                column: "employeeInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformationHistories_spouseId",
                table: "AddressInformationHistories",
                column: "spouseId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformationHistories_thanaId",
                table: "AddressInformationHistories",
                column: "thanaId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformationHistories_unionWardId",
                table: "AddressInformationHistories",
                column: "unionWardId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformationHistories_villageId",
                table: "AddressInformationHistories",
                column: "villageId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentHistories_UpdateUserId",
                table: "AssignmentHistories",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentHistories_assignmentId",
                table: "AssignmentHistories",
                column: "assignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentHistories_departmentId",
                table: "AssignmentHistories",
                column: "departmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentHistories_designationId",
                table: "AssignmentHistories",
                column: "designationId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentHistories_employeeId",
                table: "AssignmentHistories",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentHistories_rankId",
                table: "AssignmentHistories",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentHistories_sectionId",
                table: "AssignmentHistories",
                column: "sectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentHistories_specialBranchUnitId",
                table: "AssignmentHistories",
                column: "specialBranchUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardEntryHistories_UpdateUserId",
                table: "AwardEntryHistories",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardEntryHistories_awardEntryId",
                table: "AwardEntryHistories",
                column: "awardEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardEntryHistories_awardId",
                table: "AwardEntryHistories",
                column: "awardId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardEntryHistories_employeeId",
                table: "AwardEntryHistories",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualificationHistories_UpdateUserId",
                table: "EducationalQualificationHistories",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualificationHistories_degreeId",
                table: "EducationalQualificationHistories",
                column: "degreeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualificationHistories_educationalQualificationId",
                table: "EducationalQualificationHistories",
                column: "educationalQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualificationHistories_employeeId",
                table: "EducationalQualificationHistories",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualificationHistories_organizationId",
                table: "EducationalQualificationHistories",
                column: "organizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualificationHistories_reldegreesubjectId",
                table: "EducationalQualificationHistories",
                column: "reldegreesubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualificationHistories_resultId",
                table: "EducationalQualificationHistories",
                column: "resultId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_ApplicationUserId",
                table: "EmployeeInfoHistories",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_UpdateUserId",
                table: "EmployeeInfoHistories",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_bCSBatchId",
                table: "EmployeeInfoHistories",
                column: "bCSBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_banksId",
                table: "EmployeeInfoHistories",
                column: "banksId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_branchId",
                table: "EmployeeInfoHistories",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_departmentId",
                table: "EmployeeInfoHistories",
                column: "departmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_designationsId",
                table: "EmployeeInfoHistories",
                column: "designationsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_employeeTypeId",
                table: "EmployeeInfoHistories",
                column: "employeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_otherBanksId",
                table: "EmployeeInfoHistories",
                column: "otherBanksId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_rankId",
                table: "EmployeeInfoHistories",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_religionId",
                table: "EmployeeInfoHistories",
                column: "religionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_sectionId",
                table: "EmployeeInfoHistories",
                column: "sectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ForeignTravelHistories_UpdateUserId",
                table: "ForeignTravelHistories",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ForeignTravelHistories_countryId",
                table: "ForeignTravelHistories",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_ForeignTravelHistories_employeeId",
                table: "ForeignTravelHistories",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ForeignTravelHistories_foreignTravelId",
                table: "ForeignTravelHistories",
                column: "foreignTravelId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogHistories_UpdateUserId",
                table: "PromotionLogHistories",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogHistories_designationNewId",
                table: "PromotionLogHistories",
                column: "designationNewId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogHistories_designationOldId",
                table: "PromotionLogHistories",
                column: "designationOldId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogHistories_employeeId",
                table: "PromotionLogHistories",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogHistories_payScaleId",
                table: "PromotionLogHistories",
                column: "payScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogHistories_promotionLogId",
                table: "PromotionLogHistories",
                column: "promotionLogId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogHistories_rankId",
                table: "PromotionLogHistories",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogHistories_rankOldId",
                table: "PromotionLogHistories",
                column: "rankOldId");

            migrationBuilder.CreateIndex(
                name: "IX_SpouseHistories_UpdateUserId",
                table: "SpouseHistories",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpouseHistories_districtId",
                table: "SpouseHistories",
                column: "districtId");

            migrationBuilder.CreateIndex(
                name: "IX_SpouseHistories_employeeId",
                table: "SpouseHistories",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpouseHistories_spouseId",
                table: "SpouseHistories",
                column: "spouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SpouseHistories_spouseRelationId",
                table: "SpouseHistories",
                column: "spouseRelationId");

            migrationBuilder.CreateIndex(
                name: "IX_TraningLogHistories_UpdateUserId",
                table: "TraningLogHistories",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TraningLogHistories_countryId",
                table: "TraningLogHistories",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_TraningLogHistories_employeeId",
                table: "TraningLogHistories",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TraningLogHistories_trainingCategoryId",
                table: "TraningLogHistories",
                column: "trainingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TraningLogHistories_trainingInstituteId",
                table: "TraningLogHistories",
                column: "trainingInstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_TraningLogHistories_traningLogId",
                table: "TraningLogHistories",
                column: "traningLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressInformation_EmployeeInfoHistories_EmployeeInfoHistoryId",
                table: "AddressInformation",
                column: "EmployeeInfoHistoryId",
                principalTable: "EmployeeInfoHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photographs_EmployeeInfoHistories_EmployeeInfoHistoryId",
                table: "Photographs",
                column: "EmployeeInfoHistoryId",
                principalTable: "EmployeeInfoHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressInformation_EmployeeInfoHistories_EmployeeInfoHistoryId",
                table: "AddressInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_Photographs_EmployeeInfoHistories_EmployeeInfoHistoryId",
                table: "Photographs");

            migrationBuilder.DropTable(
                name: "AddressInformationHistories");

            migrationBuilder.DropTable(
                name: "AssignmentHistories");

            migrationBuilder.DropTable(
                name: "AwardEntryHistories");

            migrationBuilder.DropTable(
                name: "EducationalQualificationHistories");

            migrationBuilder.DropTable(
                name: "EmployeeInfoHistories");

            migrationBuilder.DropTable(
                name: "ForeignTravelHistories");

            migrationBuilder.DropTable(
                name: "PromotionLogHistories");

            migrationBuilder.DropTable(
                name: "SpouseHistories");

            migrationBuilder.DropTable(
                name: "TraningLogHistories");

            migrationBuilder.DropIndex(
                name: "IX_Photographs_EmployeeInfoHistoryId",
                table: "Photographs");

            migrationBuilder.DropIndex(
                name: "IX_AddressInformation_EmployeeInfoHistoryId",
                table: "AddressInformation");

            migrationBuilder.DropColumn(
                name: "EmployeeInfoHistoryId",
                table: "Photographs");

            migrationBuilder.DropColumn(
                name: "EmployeeInfoHistoryId",
                table: "AddressInformation");
        }
    }
}
