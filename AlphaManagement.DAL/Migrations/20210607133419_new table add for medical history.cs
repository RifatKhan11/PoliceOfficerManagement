using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class newtableaddformedicalhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "referenceNumber",
                table: "TraningLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "remarks",
                table: "ForeignTravels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "ForeignTravels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "referenceNumber",
                table: "DisciplinaryActions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "referenceNumber",
                table: "AwardEntries",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    diseaseName = table.Column<string>(type: "NVARCHAR(300)", nullable: false),
                    diseaseNameBn = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    diseaseId = table.Column<int>(nullable: true),
                    date = table.Column<DateTime>(nullable: true),
                    recoverydate = table.Column<DateTime>(nullable: true),
                    checkUpDate = table.Column<DateTime>(nullable: true),
                    isHospitalise = table.Column<int>(nullable: true),
                    hospitalName = table.Column<string>(nullable: true),
                    referanceDoctor = table.Column<string>(nullable: true),
                    lastCheckupHistory = table.Column<string>(nullable: true),
                    satatus = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalInfos_Diseases_diseaseId",
                        column: x => x.diseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInfos_diseaseId",
                table: "MedicalInfos",
                column: "diseaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalInfos");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropColumn(
                name: "referenceNumber",
                table: "TraningLogs");

            migrationBuilder.DropColumn(
                name: "remarks",
                table: "ForeignTravels");

            migrationBuilder.DropColumn(
                name: "status",
                table: "ForeignTravels");

            migrationBuilder.DropColumn(
                name: "referenceNumber",
                table: "DisciplinaryActions");

            migrationBuilder.DropColumn(
                name: "referenceNumber",
                table: "AwardEntries");
        }
    }
}
