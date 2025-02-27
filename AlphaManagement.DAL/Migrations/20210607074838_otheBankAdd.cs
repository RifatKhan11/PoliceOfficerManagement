using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class otheBankAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "otherBankAccountNo",
                table: "EmployeeInfos",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "otherBanksId",
                table: "EmployeeInfos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfos_otherBanksId",
                table: "EmployeeInfos",
                column: "otherBanksId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInfos_Banks_otherBanksId",
                table: "EmployeeInfos",
                column: "otherBanksId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInfos_Banks_otherBanksId",
                table: "EmployeeInfos");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInfos_otherBanksId",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "otherBankAccountNo",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "otherBanksId",
                table: "EmployeeInfos");
        }
    }
}
