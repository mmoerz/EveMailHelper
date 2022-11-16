using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class MailLabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FromId",
                table: "EveMails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("9FB1DFC3-47F7-4A25-1365-08DAC74FF425"));

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "EveMails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "EveMailLabels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveLabelId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnreadCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EveMailLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EveMailLabels_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EveMails_FromId",
                table: "EveMails",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_EveMailLabels_CharacterId",
                table: "EveMailLabels",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_EveMails_Character_FromId",
                table: "EveMails",
                column: "FromId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EveMails_Character_FromId",
                table: "EveMails");

            migrationBuilder.DropTable(
                name: "EveMailLabels");

            migrationBuilder.DropIndex(
                name: "IX_EveMails_FromId",
                table: "EveMails");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "EveMails");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "EveMails");
        }
    }
}
