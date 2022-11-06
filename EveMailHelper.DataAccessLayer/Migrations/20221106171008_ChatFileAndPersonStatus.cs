using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class ChatFileAndPersonStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChatFileId",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Character",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "None");

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

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ChatFileId",
                table: "Chats",
                column: "ChatFileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_ChatFile_ChatFileId",
                table: "Chats",
                column: "ChatFileId",
                principalTable: "ChatFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_ChatFile_ChatFileId",
                table: "Chats");

            migrationBuilder.DropTable(
                name: "ChatFile");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ChatFileId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ChatFileId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Character");
        }
    }
}
