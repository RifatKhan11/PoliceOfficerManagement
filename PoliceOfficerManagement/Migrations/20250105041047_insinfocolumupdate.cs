using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoliceOfficerManagement.Migrations
{
    /// <inheritdoc />
    public partial class insinfocolumupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "instituteType",
                table: "InstitutionInfos",
                type: "int",
                maxLength: 420,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "instituteType",
                table: "InstitutionInfos");
        }
    }
}
