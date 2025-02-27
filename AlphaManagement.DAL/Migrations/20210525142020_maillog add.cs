using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaManagement.DAL.Migrations
{
    public partial class maillogadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "otpCode",
                table: "AspNetUsers",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MailLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    sender = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    recipient = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    mailType = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    subject = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    sendTime = table.Column<DateTime>(nullable: true),
                    refNo = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    notSendReason = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    isSuccess = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailLogs");

            migrationBuilder.DropColumn(
                name: "otpCode",
                table: "AspNetUsers");
        }
    }
}
