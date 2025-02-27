using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class Vaccineanddiseasemodify_22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMedicalVaccines_Diseases_diseaseId",
                table: "EmployeeMedicalVaccines");

            migrationBuilder.RenameColumn(
                name: "diseaseId",
                table: "EmployeeMedicalVaccines",
                newName: "vaccinesId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeMedicalVaccines_diseaseId",
                table: "EmployeeMedicalVaccines",
                newName: "IX_EmployeeMedicalVaccines_vaccinesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMedicalVaccines_Vaccines_vaccinesId",
                table: "EmployeeMedicalVaccines",
                column: "vaccinesId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMedicalVaccines_Vaccines_vaccinesId",
                table: "EmployeeMedicalVaccines");

            migrationBuilder.RenameColumn(
                name: "vaccinesId",
                table: "EmployeeMedicalVaccines",
                newName: "diseaseId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeMedicalVaccines_vaccinesId",
                table: "EmployeeMedicalVaccines",
                newName: "IX_EmployeeMedicalVaccines_diseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMedicalVaccines_Diseases_diseaseId",
                table: "EmployeeMedicalVaccines",
                column: "diseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
