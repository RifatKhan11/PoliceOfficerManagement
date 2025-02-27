using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class empIddisecestbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "employeeInfoId",
                table: "MedicalInfos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInfos_employeeInfoId",
                table: "MedicalInfos",
                column: "employeeInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalInfos_EmployeeInfos_employeeInfoId",
                table: "MedicalInfos",
                column: "employeeInfoId",
                principalTable: "EmployeeInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalInfos_EmployeeInfos_employeeInfoId",
                table: "MedicalInfos");

            migrationBuilder.DropIndex(
                name: "IX_MedicalInfos_employeeInfoId",
                table: "MedicalInfos");

            migrationBuilder.DropColumn(
                name: "employeeInfoId",
                table: "MedicalInfos");
        }
    }
}
