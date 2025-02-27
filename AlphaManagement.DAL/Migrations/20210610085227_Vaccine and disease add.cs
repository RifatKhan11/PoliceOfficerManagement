using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class Vaccineanddiseaseadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalInfos_Diseases_diseaseId",
                table: "MedicalInfos");

            migrationBuilder.DropIndex(
                name: "IX_MedicalInfos_diseaseId",
                table: "MedicalInfos");

            migrationBuilder.DropColumn(
                name: "diseaseId",
                table: "MedicalInfos");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "Diseases",
                type: "NVARCHAR(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "diseaseNameBn",
                table: "Diseases",
                type: "NVARCHAR(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DiseaseGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    groupName = table.Column<string>(type: "NVARCHAR(300)", nullable: true),
                    groupNameBn = table.Column<string>(type: "NVARCHAR(300)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true),
                    statusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMedicalDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    medicalId = table.Column<int>(nullable: true),
                    diseaseId = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(type: "NVARCHAR(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMedicalDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeMedicalDiseases_Diseases_diseaseId",
                        column: x => x.diseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeMedicalDiseases_MedicalInfos_medicalId",
                        column: x => x.medicalId,
                        principalTable: "MedicalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMedicalVaccines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    medicalId = table.Column<int>(nullable: true),
                    diseaseId = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(type: "NVARCHAR(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMedicalVaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeMedicalVaccines_Diseases_diseaseId",
                        column: x => x.diseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeMedicalVaccines_MedicalInfos_medicalId",
                        column: x => x.medicalId,
                        principalTable: "MedicalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VaccineGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    groupName = table.Column<string>(type: "NVARCHAR(300)", nullable: true),
                    groupNameBn = table.Column<string>(type: "NVARCHAR(300)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true),
                    statusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    vaccineName = table.Column<string>(type: "NVARCHAR(300)", nullable: true),
                    vaccineNameBn = table.Column<string>(type: "NVARCHAR(300)", nullable: true),
                    statusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMedicalDiseases_diseaseId",
                table: "EmployeeMedicalDiseases",
                column: "diseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMedicalDiseases_medicalId",
                table: "EmployeeMedicalDiseases",
                column: "medicalId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMedicalVaccines_diseaseId",
                table: "EmployeeMedicalVaccines",
                column: "diseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMedicalVaccines_medicalId",
                table: "EmployeeMedicalVaccines",
                column: "medicalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiseaseGroups");

            migrationBuilder.DropTable(
                name: "EmployeeMedicalDiseases");

            migrationBuilder.DropTable(
                name: "EmployeeMedicalVaccines");

            migrationBuilder.DropTable(
                name: "VaccineGroups");

            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.AddColumn<int>(
                name: "diseaseId",
                table: "MedicalInfos",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "Diseases",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "diseaseNameBn",
                table: "Diseases",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(300)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInfos_diseaseId",
                table: "MedicalInfos",
                column: "diseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalInfos_Diseases_diseaseId",
                table: "MedicalInfos",
                column: "diseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
