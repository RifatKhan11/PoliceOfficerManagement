using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class empdatamodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_PoliceSubUnits_policeSubUnitId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_PoliceUnits_policeUnitId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "policeUnitId",
                table: "Assignments",
                newName: "specialBranchUnitId");

            migrationBuilder.RenameColumn(
                name: "policeSubUnitId",
                table: "Assignments",
                newName: "sectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_policeUnitId",
                table: "Assignments",
                newName: "IX_Assignments_specialBranchUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_policeSubUnitId",
                table: "Assignments",
                newName: "IX_Assignments_sectionId");

            migrationBuilder.AddColumn<string>(
                name: "trainingType",
                table: "TraningLogs",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "birthCertificate",
                table: "Spouses",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fatherName",
                table: "Spouses",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "maritalStatus",
                table: "Spouses",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "motherName",
                table: "Spouses",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "remarks",
                table: "Spouses",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rankId",
                table: "PromotionLogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rankOldId",
                table: "PromotionLogs",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Assignments",
                type: "NVARCHAR(350)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rankId",
                table: "Assignments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "servicePeriod",
                table: "Assignments",
                type: "NVARCHAR(150)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogs_rankId",
                table: "PromotionLogs",
                column: "rankId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionLogs_rankOldId",
                table: "PromotionLogs",
                column: "rankOldId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_rankId",
                table: "Assignments",
                column: "rankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Ranks_rankId",
                table: "Assignments",
                column: "rankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Sections_sectionId",
                table: "Assignments",
                column: "sectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_SpecialBranchUnits_specialBranchUnitId",
                table: "Assignments",
                column: "specialBranchUnitId",
                principalTable: "SpecialBranchUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionLogs_Ranks_rankId",
                table: "PromotionLogs",
                column: "rankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionLogs_Ranks_rankOldId",
                table: "PromotionLogs",
                column: "rankOldId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Ranks_rankId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Sections_sectionId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_SpecialBranchUnits_specialBranchUnitId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionLogs_Ranks_rankId",
                table: "PromotionLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionLogs_Ranks_rankOldId",
                table: "PromotionLogs");

            migrationBuilder.DropIndex(
                name: "IX_PromotionLogs_rankId",
                table: "PromotionLogs");

            migrationBuilder.DropIndex(
                name: "IX_PromotionLogs_rankOldId",
                table: "PromotionLogs");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_rankId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "trainingType",
                table: "TraningLogs");

            migrationBuilder.DropColumn(
                name: "birthCertificate",
                table: "Spouses");

            migrationBuilder.DropColumn(
                name: "fatherName",
                table: "Spouses");

            migrationBuilder.DropColumn(
                name: "maritalStatus",
                table: "Spouses");

            migrationBuilder.DropColumn(
                name: "motherName",
                table: "Spouses");

            migrationBuilder.DropColumn(
                name: "remarks",
                table: "Spouses");

            migrationBuilder.DropColumn(
                name: "rankId",
                table: "PromotionLogs");

            migrationBuilder.DropColumn(
                name: "rankOldId",
                table: "PromotionLogs");

            migrationBuilder.DropColumn(
                name: "rankId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "servicePeriod",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "specialBranchUnitId",
                table: "Assignments",
                newName: "policeUnitId");

            migrationBuilder.RenameColumn(
                name: "sectionId",
                table: "Assignments",
                newName: "policeSubUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_specialBranchUnitId",
                table: "Assignments",
                newName: "IX_Assignments_policeUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_sectionId",
                table: "Assignments",
                newName: "IX_Assignments_policeSubUnitId");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Assignments",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(350)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_PoliceSubUnits_policeSubUnitId",
                table: "Assignments",
                column: "policeSubUnitId",
                principalTable: "PoliceSubUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_PoliceUnits_policeUnitId",
                table: "Assignments",
                column: "policeUnitId",
                principalTable: "PoliceUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
