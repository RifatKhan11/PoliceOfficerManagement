using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class trtypecountyaddinhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "attachmentBranchId",
                table: "EmployeeInfoHistories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "countryId",
                table: "EmployeeInfoHistories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pHQTRTypeId",
                table: "EmployeeInfoHistories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_attachmentBranchId",
                table: "EmployeeInfoHistories",
                column: "attachmentBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_countryId",
                table: "EmployeeInfoHistories",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoHistories_pHQTRTypeId",
                table: "EmployeeInfoHistories",
                column: "pHQTRTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInfoHistories_SpecialBranchUnits_attachmentBranchId",
                table: "EmployeeInfoHistories",
                column: "attachmentBranchId",
                principalTable: "SpecialBranchUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInfoHistories_Countries_countryId",
                table: "EmployeeInfoHistories",
                column: "countryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInfoHistories_PHQTRTypes_pHQTRTypeId",
                table: "EmployeeInfoHistories",
                column: "pHQTRTypeId",
                principalTable: "PHQTRTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInfoHistories_SpecialBranchUnits_attachmentBranchId",
                table: "EmployeeInfoHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInfoHistories_Countries_countryId",
                table: "EmployeeInfoHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInfoHistories_PHQTRTypes_pHQTRTypeId",
                table: "EmployeeInfoHistories");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInfoHistories_attachmentBranchId",
                table: "EmployeeInfoHistories");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInfoHistories_countryId",
                table: "EmployeeInfoHistories");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInfoHistories_pHQTRTypeId",
                table: "EmployeeInfoHistories");

            migrationBuilder.DropColumn(
                name: "attachmentBranchId",
                table: "EmployeeInfoHistories");

            migrationBuilder.DropColumn(
                name: "countryId",
                table: "EmployeeInfoHistories");

            migrationBuilder.DropColumn(
                name: "pHQTRTypeId",
                table: "EmployeeInfoHistories");
        }
    }
}
