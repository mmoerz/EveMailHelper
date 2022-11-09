using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class EveSSO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_ChatFile_ChatFileId",
                table: "Chats");

            migrationBuilder.DropTable(
                name: "ChatFile");

            migrationBuilder.AddColumn<int>(
                name: "EveId",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CharacterAuthInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "random number for added security"),
                    CharId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccessToken = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false, comment: "oauth accesstoken"),
                    RefreshToken = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false, comment: "oauth refreshtoken"),
                    TokenType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "??"),
                    Scopes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAuthInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterAuthInfos_Character_CharId",
                        column: x => x.CharId,
                        principalTable: "Character",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatFiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAuthInfos_CharId",
                table: "CharacterAuthInfos",
                column: "CharId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_ChatFiles_ChatFileId",
                table: "Chats",
                column: "ChatFileId",
                principalTable: "ChatFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_ChatFiles_ChatFileId",
                table: "Chats");

            migrationBuilder.DropTable(
                name: "CharacterAuthInfos");

            migrationBuilder.DropTable(
                name: "ChatFiles");

            migrationBuilder.DropColumn(
                name: "EveId",
                table: "Character");

            migrationBuilder.CreateTable(
                name: "ChatFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatFile", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_ChatFile_ChatFileId",
                table: "Chats",
                column: "ChatFileId",
                principalTable: "ChatFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
