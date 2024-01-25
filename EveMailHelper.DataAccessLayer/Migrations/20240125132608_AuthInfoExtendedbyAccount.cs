using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class AuthInfoExtendedbyAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EveMailLabels_Character_CharacterId",
                table: "EveMailLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_EveMailLabels_Mail_MailId",
                table: "EveMailLabels");

            migrationBuilder.DropTable(
                name: "EveMailRecipient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EveMailLabels",
                table: "EveMailLabels");

            migrationBuilder.RenameTable(
                name: "EveMailLabels",
                newName: "MailLabels");

            migrationBuilder.RenameIndex(
                name: "IX_EveMailLabels_MailId",
                table: "MailLabels",
                newName: "IX_MailLabels_MailId");

            migrationBuilder.RenameIndex(
                name: "IX_EveMailLabels_CharacterId",
                table: "MailLabels",
                newName: "IX_MailLabels_CharacterId");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "CharacterAuthInfos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MailLabels",
                table: "MailLabels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MailRecipients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailRecipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailRecipients_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MailRecipients_Mail_MailId",
                        column: x => x.MailId,
                        principalSchema: "Eve",
                        principalTable: "Mail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 1, 25, 14, 26, 8, 254, DateTimeKind.Local).AddTicks(114), new DateTime(2024, 1, 25, 13, 26, 8, 254, DateTimeKind.Utc).AddTicks(90) });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAuthInfos_AccountId",
                table: "CharacterAuthInfos",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_MailRecipients_CharacterId",
                table: "MailRecipients",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_MailRecipients_MailId",
                table: "MailRecipients",
                column: "MailId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterAuthInfos_Account_AccountId",
                table: "CharacterAuthInfos",
                column: "AccountId",
                principalSchema: "Security",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MailLabels_Character_CharacterId",
                table: "MailLabels",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MailLabels_Mail_MailId",
                table: "MailLabels",
                column: "MailId",
                principalSchema: "Eve",
                principalTable: "Mail",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterAuthInfos_Account_AccountId",
                table: "CharacterAuthInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_MailLabels_Character_CharacterId",
                table: "MailLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_MailLabels_Mail_MailId",
                table: "MailLabels");

            migrationBuilder.DropTable(
                name: "MailRecipients");

            migrationBuilder.DropIndex(
                name: "IX_CharacterAuthInfos_AccountId",
                table: "CharacterAuthInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MailLabels",
                table: "MailLabels");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "CharacterAuthInfos");

            migrationBuilder.RenameTable(
                name: "MailLabels",
                newName: "EveMailLabels");

            migrationBuilder.RenameIndex(
                name: "IX_MailLabels_MailId",
                table: "EveMailLabels",
                newName: "IX_EveMailLabels_MailId");

            migrationBuilder.RenameIndex(
                name: "IX_MailLabels_CharacterId",
                table: "EveMailLabels",
                newName: "IX_EveMailLabels_CharacterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EveMailLabels",
                table: "EveMailLabels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EveMailRecipient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EveMailRecipient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EveMailRecipient_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EveMailRecipient_Mail_MailId",
                        column: x => x.MailId,
                        principalSchema: "Eve",
                        principalTable: "Mail",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2022, 12, 1, 20, 49, 0, 354, DateTimeKind.Local).AddTicks(4587), new DateTime(2022, 12, 1, 19, 49, 0, 354, DateTimeKind.Utc).AddTicks(4563) });

            migrationBuilder.CreateIndex(
                name: "IX_EveMailRecipient_CharacterId",
                table: "EveMailRecipient",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EveMailRecipient_MailId",
                table: "EveMailRecipient",
                column: "MailId");

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailLabels_Character_CharacterId",
                table: "EveMailLabels",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailLabels_Mail_MailId",
                table: "EveMailLabels",
                column: "MailId",
                principalSchema: "Eve",
                principalTable: "Mail",
                principalColumn: "Id");
        }
    }
}
