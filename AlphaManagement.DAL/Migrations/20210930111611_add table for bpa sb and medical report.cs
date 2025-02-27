using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class addtableforbpasbandmedicalreport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employeeReportInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    employeeInfoId = table.Column<int>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: true),
                    date = table.Column<DateTime>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeReportInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employeeReportInfos_EmployeeInfos_employeeInfoId",
                        column: x => x.employeeInfoId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "medicalMainCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    name = table.Column<string>(nullable: true),
                    nameBn = table.Column<string>(nullable: true),
                    sortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicalMainCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "medicalSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    name = table.Column<string>(nullable: true),
                    nameBn = table.Column<string>(nullable: true),
                    sortOrder = table.Column<int>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    medicalMainCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicalSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medicalSubCategories_medicalMainCategories_medicalMainCategoryId",
                        column: x => x.medicalMainCategoryId,
                        principalTable: "medicalMainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employeeMadicalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    employeeInfoId = table.Column<int>(nullable: true),
                    medicalSubCategoryId = table.Column<int>(nullable: true),
                    date = table.Column<DateTime>(nullable: true),
                    status = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeMadicalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employeeMadicalInfos_EmployeeInfos_employeeInfoId",
                        column: x => x.employeeInfoId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_employeeMadicalInfos_medicalSubCategories_medicalSubCategoryId",
                        column: x => x.medicalSubCategoryId,
                        principalTable: "medicalSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employeeMadicalInfos_employeeInfoId",
                table: "employeeMadicalInfos",
                column: "employeeInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeMadicalInfos_medicalSubCategoryId",
                table: "employeeMadicalInfos",
                column: "medicalSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeReportInfos_employeeInfoId",
                table: "employeeReportInfos",
                column: "employeeInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_medicalSubCategories_medicalMainCategoryId",
                table: "medicalSubCategories",
                column: "medicalMainCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeeMadicalInfos");

            migrationBuilder.DropTable(
                name: "employeeReportInfos");

            migrationBuilder.DropTable(
                name: "medicalSubCategories");

            migrationBuilder.DropTable(
                name: "medicalMainCategories");
        }
    }
}
