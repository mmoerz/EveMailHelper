using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class MigrationMail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EveMailLabels_EveMails_MailId",
                table: "EveMailLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_EveMailRecipient_EveMails_MailId",
                table: "EveMailRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_EveMails_Character_FromId",
                table: "EveMails");

            migrationBuilder.DropForeignKey(
                name: "FK_EveMails_EveMailTemplate_EveMailTemplateId",
                table: "EveMails");

            migrationBuilder.DropForeignKey(
                name: "FK_EveMailSentTo_EveMails_EveMailId",
                table: "EveMailSentTo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EveMails",
                table: "EveMails");

            migrationBuilder.EnsureSchema(
                name: "Eve");

            migrationBuilder.RenameTable(
                name: "EveMails",
                newName: "Mail",
                newSchema: "Eve");

            migrationBuilder.RenameIndex(
                name: "IX_EveMails_FromId",
                schema: "Eve",
                table: "Mail",
                newName: "IX_Mail_FromId");

            migrationBuilder.RenameIndex(
                name: "IX_EveMails_EveMailTemplateId",
                schema: "Eve",
                table: "Mail",
                newName: "IX_Mail_EveMailTemplateId");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                schema: "Eve",
                table: "Mail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mail",
                schema: "Eve",
                table: "Mail",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mail_OwnerId",
                schema: "Eve",
                table: "Mail",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailLabels_Mail_MailId",
                table: "EveMailLabels",
                column: "MailId",
                principalSchema: "Eve",
                principalTable: "Mail",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailRecipient_Mail_MailId",
                table: "EveMailRecipient",
                column: "MailId",
                principalSchema: "Eve",
                principalTable: "Mail",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailSentTo_Mail_EveMailId",
                table: "EveMailSentTo",
                column: "EveMailId",
                principalSchema: "Eve",
                principalTable: "Mail",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mail_Character_FromId",
                schema: "Eve",
                table: "Mail",
                column: "FromId",
                principalTable: "Character",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mail_Character_OwnerId",
                schema: "Eve",
                table: "Mail",
                column: "OwnerId",
                principalTable: "Character",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mail_EveMailTemplate_EveMailTemplateId",
                schema: "Eve",
                table: "Mail",
                column: "EveMailTemplateId",
                principalTable: "EveMailTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EveMailLabels_Mail_MailId",
                table: "EveMailLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_EveMailRecipient_Mail_MailId",
                table: "EveMailRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_EveMailSentTo_Mail_EveMailId",
                table: "EveMailSentTo");

            migrationBuilder.DropForeignKey(
                name: "FK_Mail_Character_FromId",
                schema: "Eve",
                table: "Mail");

            migrationBuilder.DropForeignKey(
                name: "FK_Mail_Character_OwnerId",
                schema: "Eve",
                table: "Mail");

            migrationBuilder.DropForeignKey(
                name: "FK_Mail_EveMailTemplate_EveMailTemplateId",
                schema: "Eve",
                table: "Mail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mail",
                schema: "Eve",
                table: "Mail");

            migrationBuilder.DropIndex(
                name: "IX_Mail_OwnerId",
                schema: "Eve",
                table: "Mail");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "Eve",
                table: "Mail");

            migrationBuilder.RenameTable(
                name: "Mail",
                schema: "Eve",
                newName: "EveMails");

            migrationBuilder.RenameIndex(
                name: "IX_Mail_FromId",
                table: "EveMails",
                newName: "IX_EveMails_FromId");

            migrationBuilder.RenameIndex(
                name: "IX_Mail_EveMailTemplateId",
                table: "EveMails",
                newName: "IX_EveMails_EveMailTemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EveMails",
                table: "EveMails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailLabels_EveMails_MailId",
                table: "EveMailLabels",
                column: "MailId",
                principalTable: "EveMails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailRecipient_EveMails_MailId",
                table: "EveMailRecipient",
                column: "MailId",
                principalTable: "EveMails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EveMails_Character_FromId",
                table: "EveMails",
                column: "FromId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EveMails_EveMailTemplate_EveMailTemplateId",
                table: "EveMails",
                column: "EveMailTemplateId",
                principalTable: "EveMailTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailSentTo_EveMails_EveMailId",
                table: "EveMailSentTo",
                column: "EveMailId",
                principalTable: "EveMails",
                principalColumn: "Id");
        }
    }
}
