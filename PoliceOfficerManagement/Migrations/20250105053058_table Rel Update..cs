using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoliceOfficerManagement.Migrations
{
    /// <inheritdoc />
    public partial class tableRelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TrainingInfos_instituteId",
                table: "TrainingInfos",
                column: "instituteId");

            migrationBuilder.CreateIndex(
                name: "IX_PostingPlaces_districtId",
                table: "PostingPlaces",
                column: "districtId");

            migrationBuilder.CreateIndex(
                name: "IX_PostingPlaces_rangeId",
                table: "PostingPlaces",
                column: "rangeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostingPlaces_thanaId",
                table: "PostingPlaces",
                column: "thanaId");

            migrationBuilder.CreateIndex(
                name: "IX_PostingPlaces_zoneId",
                table: "PostingPlaces",
                column: "zoneId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInfos_instituteId",
                table: "EducationalInfos",
                column: "instituteId");

            migrationBuilder.CreateIndex(
                name: "IX_AdderssInfos_districtId",
                table: "AdderssInfos",
                column: "districtId");

            migrationBuilder.CreateIndex(
                name: "IX_AdderssInfos_divisionId",
                table: "AdderssInfos",
                column: "divisionId");

            migrationBuilder.CreateIndex(
                name: "IX_AdderssInfos_thanaId",
                table: "AdderssInfos",
                column: "thanaId");

            migrationBuilder.CreateIndex(
                name: "IX_AdderssInfos_unionId",
                table: "AdderssInfos",
                column: "unionId");

            migrationBuilder.CreateIndex(
                name: "IX_AdderssInfos_villegeId",
                table: "AdderssInfos",
                column: "villegeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdderssInfos_Districts_districtId",
                table: "AdderssInfos",
                column: "districtId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdderssInfos_Divisions_divisionId",
                table: "AdderssInfos",
                column: "divisionId",
                principalTable: "Divisions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdderssInfos_Thanas_thanaId",
                table: "AdderssInfos",
                column: "thanaId",
                principalTable: "Thanas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdderssInfos_UnionWards_unionId",
                table: "AdderssInfos",
                column: "unionId",
                principalTable: "UnionWards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdderssInfos_Villages_villegeId",
                table: "AdderssInfos",
                column: "villegeId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalInfos_InstitutionInfos_instituteId",
                table: "EducationalInfos",
                column: "instituteId",
                principalTable: "InstitutionInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostingPlaces_DivisionDistricts_districtId",
                table: "PostingPlaces",
                column: "districtId",
                principalTable: "DivisionDistricts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostingPlaces_PoliceThanas_thanaId",
                table: "PostingPlaces",
                column: "thanaId",
                principalTable: "PoliceThanas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostingPlaces_RangeMetros_rangeId",
                table: "PostingPlaces",
                column: "rangeId",
                principalTable: "RangeMetros",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostingPlaces_ZoneCircles_zoneId",
                table: "PostingPlaces",
                column: "zoneId",
                principalTable: "ZoneCircles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingInfos_InstitutionInfos_instituteId",
                table: "TrainingInfos",
                column: "instituteId",
                principalTable: "InstitutionInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdderssInfos_Districts_districtId",
                table: "AdderssInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_AdderssInfos_Divisions_divisionId",
                table: "AdderssInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_AdderssInfos_Thanas_thanaId",
                table: "AdderssInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_AdderssInfos_UnionWards_unionId",
                table: "AdderssInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_AdderssInfos_Villages_villegeId",
                table: "AdderssInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationalInfos_InstitutionInfos_instituteId",
                table: "EducationalInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_PostingPlaces_DivisionDistricts_districtId",
                table: "PostingPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_PostingPlaces_PoliceThanas_thanaId",
                table: "PostingPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_PostingPlaces_RangeMetros_rangeId",
                table: "PostingPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_PostingPlaces_ZoneCircles_zoneId",
                table: "PostingPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingInfos_InstitutionInfos_instituteId",
                table: "TrainingInfos");

            migrationBuilder.DropIndex(
                name: "IX_TrainingInfos_instituteId",
                table: "TrainingInfos");

            migrationBuilder.DropIndex(
                name: "IX_PostingPlaces_districtId",
                table: "PostingPlaces");

            migrationBuilder.DropIndex(
                name: "IX_PostingPlaces_rangeId",
                table: "PostingPlaces");

            migrationBuilder.DropIndex(
                name: "IX_PostingPlaces_thanaId",
                table: "PostingPlaces");

            migrationBuilder.DropIndex(
                name: "IX_PostingPlaces_zoneId",
                table: "PostingPlaces");

            migrationBuilder.DropIndex(
                name: "IX_EducationalInfos_instituteId",
                table: "EducationalInfos");

            migrationBuilder.DropIndex(
                name: "IX_AdderssInfos_districtId",
                table: "AdderssInfos");

            migrationBuilder.DropIndex(
                name: "IX_AdderssInfos_divisionId",
                table: "AdderssInfos");

            migrationBuilder.DropIndex(
                name: "IX_AdderssInfos_thanaId",
                table: "AdderssInfos");

            migrationBuilder.DropIndex(
                name: "IX_AdderssInfos_unionId",
                table: "AdderssInfos");

            migrationBuilder.DropIndex(
                name: "IX_AdderssInfos_villegeId",
                table: "AdderssInfos");
        }
    }
}
