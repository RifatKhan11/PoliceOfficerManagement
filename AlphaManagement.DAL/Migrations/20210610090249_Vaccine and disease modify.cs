using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class Vaccineanddiseasemodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "vaccineGroupId",
                table: "Vaccines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "diseaseGroupId",
                table: "Diseases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_vaccineGroupId",
                table: "Vaccines",
                column: "vaccineGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_diseaseGroupId",
                table: "Diseases",
                column: "diseaseGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diseases_DiseaseGroups_diseaseGroupId",
                table: "Diseases",
                column: "diseaseGroupId",
                principalTable: "DiseaseGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_VaccineGroups_vaccineGroupId",
                table: "Vaccines",
                column: "vaccineGroupId",
                principalTable: "VaccineGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diseases_DiseaseGroups_diseaseGroupId",
                table: "Diseases");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_VaccineGroups_vaccineGroupId",
                table: "Vaccines");

            migrationBuilder.DropIndex(
                name: "IX_Vaccines_vaccineGroupId",
                table: "Vaccines");

            migrationBuilder.DropIndex(
                name: "IX_Diseases_diseaseGroupId",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "vaccineGroupId",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "diseaseGroupId",
                table: "Diseases");
        }
    }
}
