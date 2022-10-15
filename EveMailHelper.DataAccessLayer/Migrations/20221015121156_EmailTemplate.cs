using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class EmailTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EveMailTemplateId",
                table: "EveMails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EveMailTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EveMailTemplate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EveMails_EveMailTemplateId",
                table: "EveMails",
                column: "EveMailTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EveMails_EveMailTemplate_EveMailTemplateId",
                table: "EveMails",
                column: "EveMailTemplateId",
                principalTable: "EveMailTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EveMails_EveMailTemplate_EveMailTemplateId",
                table: "EveMails");

            migrationBuilder.DropTable(
                name: "EveMailTemplate");

            migrationBuilder.DropIndex(
                name: "IX_EveMails_EveMailTemplateId",
                table: "EveMails");

            migrationBuilder.DropColumn(
                name: "EveMailTemplateId",
                table: "EveMails");
        }
    }
}
