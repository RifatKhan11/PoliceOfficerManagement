using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class organogram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    nameEN = table.Column<string>(nullable: true),
                    nameBN = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganoOrganizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    organoOrganizationId = table.Column<int>(nullable: true),
                    organizationTypeId = table.Column<int>(nullable: true),
                    nameEN = table.Column<string>(nullable: true),
                    nameBN = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganoOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganoOrganizations_OrganizationTypes_organizationTypeId",
                        column: x => x.organizationTypeId,
                        principalTable: "OrganizationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganoOrganizations_OrganoOrganizations_organoOrganizationId",
                        column: x => x.organoOrganizationId,
                        principalTable: "OrganoOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    organoOrganizationId = table.Column<int>(nullable: true),
                    designationId = table.Column<int>(nullable: true),
                    altDesignationId = table.Column<int>(nullable: true),
                    IsHead = table.Column<int>(nullable: false),
                    numberOfPost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Designations_altDesignationId",
                        column: x => x.altDesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Designations_designationId",
                        column: x => x.designationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_OrganoOrganizations_organoOrganizationId",
                        column: x => x.organoOrganizationId,
                        principalTable: "OrganoOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganoOrganizations_organizationTypeId",
                table: "OrganoOrganizations",
                column: "organizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganoOrganizations_organoOrganizationId",
                table: "OrganoOrganizations",
                column: "organoOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_altDesignationId",
                table: "Posts",
                column: "altDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_designationId",
                table: "Posts",
                column: "designationId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_organoOrganizationId",
                table: "Posts",
                column: "organoOrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "OrganoOrganizations");

            migrationBuilder.DropTable(
                name: "OrganizationTypes");
        }
    }
}
