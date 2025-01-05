using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoliceOfficerManagement.Migrations
{
    /// <inheritdoc />
    public partial class isdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "ZoneCircles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "Villages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "UserTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "UnionWards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "TrainingInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "Thanas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "ranks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "RangeMetros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "PostingPlaces",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "PoliceThanas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "InstitutionInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "employeeInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "EducationalInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "Divisions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "DivisionDistricts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "Districts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "Countries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "AdderssInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "ActivityLogTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isDelete",
                table: "ActivityHistoryLogs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "ZoneCircles");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "Villages");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "UserTypes");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "UnionWards");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "TrainingInfos");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "Thanas");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "ranks");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "RangeMetros");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "PostingPlaces");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "PoliceThanas");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "InstitutionInfos");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "employeeInfos");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "EducationalInfos");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "DivisionDistricts");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "AdderssInfos");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "ActivityLogTypes");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "ActivityHistoryLogs");
        }
    }
}
