using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class empmodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bankAccount",
                table: "EmployeeInfos",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "facebookId",
                table: "EmployeeInfos",
                type: "nvarchar(350)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "height",
                table: "EmployeeInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "identificationSign",
                table: "EmployeeInfos",
                type: "nvarchar(350)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "linkdInId",
                table: "EmployeeInfos",
                type: "nvarchar(350)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "passportNo",
                table: "EmployeeInfos",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "promotionDate",
                table: "EmployeeInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "salaryAccountNo",
                table: "EmployeeInfos",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "servicePeriod",
                table: "EmployeeInfos",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tribal",
                table: "EmployeeInfos",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "weight",
                table: "EmployeeInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bankAccount",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "facebookId",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "height",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "identificationSign",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "linkdInId",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "passportNo",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "promotionDate",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "salaryAccountNo",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "servicePeriod",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "tribal",
                table: "EmployeeInfos");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "EmployeeInfos");
        }
    }
}
