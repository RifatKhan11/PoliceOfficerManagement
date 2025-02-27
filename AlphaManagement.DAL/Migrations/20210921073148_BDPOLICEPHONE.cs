using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class BDPOLICEPHONE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BDPolicePhoneBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    sectionId = table.Column<int>(nullable: true),
                    departmentId = table.Column<int>(nullable: true),
                    mobileNo = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    phoneNo = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    statusId = table.Column<int>(nullable: true),
                    typeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BDPolicePhoneBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BDPolicePhoneBooks_Departments_departmentId",
                        column: x => x.departmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BDPolicePhoneBooks_Sections_sectionId",
                        column: x => x.sectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BDPolicePhoneBooks_departmentId",
                table: "BDPolicePhoneBooks",
                column: "departmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BDPolicePhoneBooks_sectionId",
                table: "BDPolicePhoneBooks",
                column: "sectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BDPolicePhoneBooks");
        }
    }
}
