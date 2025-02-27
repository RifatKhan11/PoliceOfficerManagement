using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class travel_banksadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "banksId",
                table: "EmployeeInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "drivingLicense",
                table: "EmployeeInfos",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rationId",
                table: "EmployeeInfos",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    bankName = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    bankNameBn = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForeignTravels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    employeeId = table.Column<int>(nullable: true),
                    countryId = table.Column<int>(nullable: true),
                    travelPurpose = table.Column<string>(nullable: true),
                    travelDate = table.Column<DateTime>(nullable: true),
                    travelEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignTravels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForeignTravels_Countries_countryId",
                        column: x => x.countryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ForeignTravels_EmployeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EmployeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfos_banksId",
                table: "EmployeeInfos",
                column: "banksId");

            migrationBuilder.CreateIndex(
                name: "IX_ForeignTravels_countryId",
                table: "ForeignTravels",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_ForeignTravels_employeeId",
                table: "ForeignTravels",
                column: "employeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInfos_Banks_banksId",
                table: "EmployeeInfos",
                column: "banksId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInfos_Banks_banksId",
                table: "EmployeeInfos");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "ForeignTravels");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInfos_banksId",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "banksId",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "drivingLicense",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "rationId",
                table: "EmployeeInfos");
        }
    }
}
