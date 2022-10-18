using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class ChatAndNoteFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Character_AttachedToId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Character_ListenerId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Character_AttachedToId",
                table: "Note");

            migrationBuilder.DropTable(
                name: "ChatEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Note",
                table: "Note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat",
                table: "Chat");

            migrationBuilder.RenameTable(
                name: "Note",
                newName: "Notes");

            migrationBuilder.RenameTable(
                name: "Chat",
                newName: "Chats");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Notes",
                newName: "CreatedDate");

            migrationBuilder.RenameIndex(
                name: "IX_Note_AttachedToId",
                table: "Notes",
                newName: "IX_Notes_AttachedToId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_ListenerId",
                table: "Chats",
                newName: "IX_Chats_ListenerId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_AttachedToId",
                table: "Chats",
                newName: "IX_Chats_AttachedToId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Character_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_AuthorId",
                table: "ChatMessages",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatId",
                table: "ChatMessages",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Character_AttachedToId",
                table: "Chats",
                column: "AttachedToId",
                principalTable: "Character",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Character_ListenerId",
                table: "Chats",
                column: "ListenerId",
                principalTable: "Character",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Character_AttachedToId",
                table: "Notes",
                column: "AttachedToId",
                principalTable: "Character",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Character_AttachedToId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Character_ListenerId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Character_AttachedToId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "Note");

            migrationBuilder.RenameTable(
                name: "Chats",
                newName: "Chat");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Note",
                newName: "Created");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_AttachedToId",
                table: "Note",
                newName: "IX_Note_AttachedToId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_ListenerId",
                table: "Chat",
                newName: "IX_Chat_ListenerId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_AttachedToId",
                table: "Chat",
                newName: "IX_Chat_AttachedToId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Note",
                table: "Note",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat",
                table: "Chat",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChatEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatEntry_Character_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatEntry_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatEntry_AuthorId",
                table: "ChatEntry",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatEntry_ChatId",
                table: "ChatEntry",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Character_AttachedToId",
                table: "Chat",
                column: "AttachedToId",
                principalTable: "Character",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Character_ListenerId",
                table: "Chat",
                column: "ListenerId",
                principalTable: "Character",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Character_AttachedToId",
                table: "Note",
                column: "AttachedToId",
                principalTable: "Character",
                principalColumn: "Id");
        }
    }
}
