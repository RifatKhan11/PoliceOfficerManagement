using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoliceOfficerManagement.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLogTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typeName = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    tableName = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    shortOrder = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userTypeId = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    otpCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailOtpCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    otpExpire = table.Column<DateTime>(type: "datetime2", nullable: true),
                    emailVerifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isVarified = table.Column<int>(type: "int", nullable: true),
                    isChangePassword = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<int>(type: "int", nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    countryName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    countryNameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employeeInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameEn = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    nameBn = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    employeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    nidNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    joiningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    retirementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    joiningRankId = table.Column<int>(type: "int", nullable: true),
                    homeDistrict = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    homeUpazila = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    reMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    personalPhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    officePhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstitutionInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<int>(type: "int", nullable: true),
                    nameBn = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    nameEn = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    establishYear = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    placeInfo = table.Column<string>(type: "nvarchar(420)", maxLength: 420, nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RangeMetros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rangeMetroName = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    rangeMetroNameBn = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    pimsRangeId = table.Column<int>(type: "int", nullable: true),
                    pimsRangeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangeMetros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ranks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rankCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    rankName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    rankNameBN = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    shortName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    shortOrder = table.Column<int>(type: "int", nullable: true),
                    forceCatId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shortOrder = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityHistoryLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    logTypeId = table.Column<int>(type: "int", nullable: true),
                    actionId = table.Column<int>(type: "int", nullable: true),
                    actionName = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    description = table.Column<string>(type: "NVARCHAR(250)", nullable: true),
                    ipAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityHistoryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityHistoryLogs_ActivityLogTypes_logTypeId",
                        column: x => x.logTypeId,
                        principalTable: "ActivityLogTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivityHistoryLogs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryId = table.Column<int>(type: "int", nullable: true),
                    divisionCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    divisionName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    divisionNameBn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Divisions_Countries_countryId",
                        column: x => x.countryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdderssInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeId = table.Column<int>(type: "int", nullable: true),
                    addressType = table.Column<int>(type: "int", nullable: false),
                    roadInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    villegeId = table.Column<int>(type: "int", nullable: true),
                    unionId = table.Column<int>(type: "int", nullable: true),
                    thanaId = table.Column<int>(type: "int", nullable: true),
                    districtId = table.Column<int>(type: "int", nullable: true),
                    divisionId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdderssInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdderssInfos_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EducationalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    instituteId = table.Column<int>(type: "int", nullable: true),
                    passingYear = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    batchNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    grade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    degreeName = table.Column<string>(type: "nvarchar(420)", maxLength: 420, nullable: true),
                    achievement = table.Column<string>(type: "nvarchar(420)", maxLength: 420, nullable: true),
                    courseDuration = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    employeeId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationalInfos_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostingPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rankId = table.Column<int>(type: "int", nullable: true),
                    postingFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    postingTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    thanaId = table.Column<int>(type: "int", nullable: true),
                    zoneId = table.Column<int>(type: "int", nullable: true),
                    districtId = table.Column<int>(type: "int", nullable: true),
                    rangeId = table.Column<int>(type: "int", nullable: true),
                    reMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    promotionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    employeeId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostingPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostingPlaces_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrainingInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    instituteId = table.Column<int>(type: "int", nullable: true),
                    passingYear = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    batchNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    grade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    degreeName = table.Column<string>(type: "nvarchar(420)", maxLength: 420, nullable: true),
                    achievement = table.Column<string>(type: "nvarchar(420)", maxLength: 420, nullable: true),
                    courseDuration = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    employeeId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingInfos_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DivisionDistricts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rangeMetroId = table.Column<int>(type: "int", nullable: true),
                    divisionDistrictName = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    divisionDistrictNameBn = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    pimsDistrictId = table.Column<int>(type: "int", nullable: true),
                    pimsDistrictName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionDistricts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DivisionDistricts_RangeMetros_rangeMetroId",
                        column: x => x.rangeMetroId,
                        principalTable: "RangeMetros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    divisionId = table.Column<int>(type: "int", nullable: false),
                    districtCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    districtName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    districtNameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Divisions_divisionId",
                        column: x => x.divisionId,
                        principalTable: "Divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZoneCircles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    divisionDistrictId = table.Column<int>(type: "int", nullable: true),
                    zoneName = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    zoneNameBn = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    pimsZoneId = table.Column<int>(type: "int", nullable: true),
                    pimsZoneName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneCircles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneCircles_DivisionDistricts_divisionDistrictId",
                        column: x => x.divisionDistrictId,
                        principalTable: "DivisionDistricts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Thanas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    districtId = table.Column<int>(type: "int", nullable: true),
                    rangeMetroId = table.Column<int>(type: "int", nullable: true),
                    thanaCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    thanaName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    thanaNameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thanas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thanas_Districts_districtId",
                        column: x => x.districtId,
                        principalTable: "Districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Thanas_RangeMetros_rangeMetroId",
                        column: x => x.rangeMetroId,
                        principalTable: "RangeMetros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PoliceThanas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rangeMetroId = table.Column<int>(type: "int", nullable: true),
                    divisionDistrictId = table.Column<int>(type: "int", nullable: true),
                    zoneCircleId = table.Column<int>(type: "int", nullable: true),
                    upazillaId = table.Column<int>(type: "int", nullable: true),
                    policeThanaName = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    policeThanaNameBn = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    isReportable = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    policeThanaId = table.Column<int>(type: "int", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fariType = table.Column<int>(type: "int", nullable: true),
                    isChild = table.Column<int>(type: "int", nullable: true),
                    pimsThanaId = table.Column<int>(type: "int", nullable: true),
                    pimsThanaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceThanas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceThanas_DivisionDistricts_divisionDistrictId",
                        column: x => x.divisionDistrictId,
                        principalTable: "DivisionDistricts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PoliceThanas_PoliceThanas_policeThanaId",
                        column: x => x.policeThanaId,
                        principalTable: "PoliceThanas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PoliceThanas_RangeMetros_rangeMetroId",
                        column: x => x.rangeMetroId,
                        principalTable: "RangeMetros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PoliceThanas_Thanas_upazillaId",
                        column: x => x.upazillaId,
                        principalTable: "Thanas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PoliceThanas_ZoneCircles_zoneCircleId",
                        column: x => x.zoneCircleId,
                        principalTable: "ZoneCircles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnionWards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    thanaId = table.Column<int>(type: "int", nullable: false),
                    districtsId = table.Column<int>(type: "int", nullable: true),
                    unionCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    unionName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    unionNameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnionWards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnionWards_Districts_districtsId",
                        column: x => x.districtsId,
                        principalTable: "Districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnionWards_Thanas_thanaId",
                        column: x => x.thanaId,
                        principalTable: "Thanas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Villages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    unionWardId = table.Column<int>(type: "int", nullable: false),
                    thanaId = table.Column<int>(type: "int", nullable: true),
                    districtsId = table.Column<int>(type: "int", nullable: true),
                    villageCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    villageName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    villageNameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Villages_Districts_districtsId",
                        column: x => x.districtsId,
                        principalTable: "Districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Villages_Thanas_thanaId",
                        column: x => x.thanaId,
                        principalTable: "Thanas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Villages_UnionWards_unionWardId",
                        column: x => x.unionWardId,
                        principalTable: "UnionWards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistoryLogs_ApplicationUserId",
                table: "ActivityHistoryLogs",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistoryLogs_logTypeId",
                table: "ActivityHistoryLogs",
                column: "logTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdderssInfos_employeeId",
                table: "AdderssInfos",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_divisionId",
                table: "Districts",
                column: "divisionId");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionDistricts_rangeMetroId",
                table: "DivisionDistricts",
                column: "rangeMetroId");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_countryId",
                table: "Divisions",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInfos_employeeId",
                table: "EducationalInfos",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceThanas_divisionDistrictId",
                table: "PoliceThanas",
                column: "divisionDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceThanas_policeThanaId",
                table: "PoliceThanas",
                column: "policeThanaId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceThanas_rangeMetroId",
                table: "PoliceThanas",
                column: "rangeMetroId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceThanas_upazillaId",
                table: "PoliceThanas",
                column: "upazillaId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceThanas_zoneCircleId",
                table: "PoliceThanas",
                column: "zoneCircleId");

            migrationBuilder.CreateIndex(
                name: "IX_PostingPlaces_employeeId",
                table: "PostingPlaces",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Thanas_districtId",
                table: "Thanas",
                column: "districtId");

            migrationBuilder.CreateIndex(
                name: "IX_Thanas_rangeMetroId",
                table: "Thanas",
                column: "rangeMetroId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingInfos_employeeId",
                table: "TrainingInfos",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnionWards_districtsId",
                table: "UnionWards",
                column: "districtsId");

            migrationBuilder.CreateIndex(
                name: "IX_UnionWards_thanaId",
                table: "UnionWards",
                column: "thanaId");

            migrationBuilder.CreateIndex(
                name: "IX_Villages_districtsId",
                table: "Villages",
                column: "districtsId");

            migrationBuilder.CreateIndex(
                name: "IX_Villages_thanaId",
                table: "Villages",
                column: "thanaId");

            migrationBuilder.CreateIndex(
                name: "IX_Villages_unionWardId",
                table: "Villages",
                column: "unionWardId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneCircles_divisionDistrictId",
                table: "ZoneCircles",
                column: "divisionDistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityHistoryLogs");

            migrationBuilder.DropTable(
                name: "AdderssInfos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EducationalInfos");

            migrationBuilder.DropTable(
                name: "InstitutionInfos");

            migrationBuilder.DropTable(
                name: "PoliceThanas");

            migrationBuilder.DropTable(
                name: "PostingPlaces");

            migrationBuilder.DropTable(
                name: "ranks");

            migrationBuilder.DropTable(
                name: "TrainingInfos");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropTable(
                name: "Villages");

            migrationBuilder.DropTable(
                name: "ActivityLogTypes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ZoneCircles");

            migrationBuilder.DropTable(
                name: "employeeInfos");

            migrationBuilder.DropTable(
                name: "UnionWards");

            migrationBuilder.DropTable(
                name: "DivisionDistricts");

            migrationBuilder.DropTable(
                name: "Thanas");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "RangeMetros");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
