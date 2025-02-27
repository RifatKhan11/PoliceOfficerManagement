using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class supervisorAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ministryRefNo",
                table: "Assignments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reasonOfTransfer",
                table: "Assignments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "receiveRefNo",
                table: "Assignments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "supervisorId",
                table: "Assignments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_supervisorId",
                table: "Assignments",
                column: "supervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_EmployeeInfos_supervisorId",
                table: "Assignments",
                column: "supervisorId",
                principalTable: "EmployeeInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_EmployeeInfos_supervisorId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_supervisorId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "ministryRefNo",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "reasonOfTransfer",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "receiveRefNo",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "supervisorId",
                table: "Assignments");
        }
    }
}
