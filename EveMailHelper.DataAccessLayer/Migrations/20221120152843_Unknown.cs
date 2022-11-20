using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class Unknown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EveMailLabels_EveMails_EveMailId",
                table: "EveMailLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_EveMailRecipient_EveMails_EveMailId",
                table: "EveMailRecipient");

            migrationBuilder.RenameColumn(
                name: "EveMailId",
                table: "EveMails",
                newName: "EveId");

            migrationBuilder.RenameColumn(
                name: "EveMailId",
                table: "EveMailRecipient",
                newName: "MailId");

            migrationBuilder.RenameIndex(
                name: "IX_EveMailRecipient_EveMailId",
                table: "EveMailRecipient",
                newName: "IX_EveMailRecipient_MailId");

            migrationBuilder.RenameColumn(
                name: "EveMailId",
                table: "EveMailLabels",
                newName: "MailId");

            migrationBuilder.RenameIndex(
                name: "IX_EveMailLabels_EveMailId",
                table: "EveMailLabels",
                newName: "IX_EveMailLabels_MailId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFounded",
                table: "Alliances",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "MailLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailLists", x => x.Id);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EveMailLabels_EveMails_MailId",
                table: "EveMailLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_EveMailRecipient_EveMails_MailId",
                table: "EveMailRecipient");

            migrationBuilder.DropTable(
                name: "MailLists");

            migrationBuilder.RenameColumn(
                name: "EveId",
                table: "EveMails",
                newName: "EveMailId");

            migrationBuilder.RenameColumn(
                name: "MailId",
                table: "EveMailRecipient",
                newName: "EveMailId");

            migrationBuilder.RenameIndex(
                name: "IX_EveMailRecipient_MailId",
                table: "EveMailRecipient",
                newName: "IX_EveMailRecipient_EveMailId");

            migrationBuilder.RenameColumn(
                name: "MailId",
                table: "EveMailLabels",
                newName: "EveMailId");

            migrationBuilder.RenameIndex(
                name: "IX_EveMailLabels_MailId",
                table: "EveMailLabels",
                newName: "IX_EveMailLabels_EveMailId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFounded",
                table: "Alliances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailLabels_EveMails_EveMailId",
                table: "EveMailLabels",
                column: "EveMailId",
                principalTable: "EveMails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailRecipient_EveMails_EveMailId",
                table: "EveMailRecipient",
                column: "EveMailId",
                principalTable: "EveMails",
                principalColumn: "Id");
        }
    }
}
