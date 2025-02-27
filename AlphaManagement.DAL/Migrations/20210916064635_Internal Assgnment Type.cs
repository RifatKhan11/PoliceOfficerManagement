using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class InternalAssgnmentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalAssignmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    typeName = table.Column<string>(nullable: true),
                    typeNameBn = table.Column<string>(nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalAssignmentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalAssignments_assignmentTypeId",
                table: "InternalAssignments",
                column: "assignmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalAssignments_InternalAssignmentTypes_assignmentTypeId",
                table: "InternalAssignments",
                column: "assignmentTypeId",
                principalTable: "InternalAssignmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalAssignments_InternalAssignmentTypes_assignmentTypeId",
                table: "InternalAssignments");

            migrationBuilder.DropTable(
                name: "InternalAssignmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_InternalAssignments_assignmentTypeId",
                table: "InternalAssignments");
        }
    }
}
