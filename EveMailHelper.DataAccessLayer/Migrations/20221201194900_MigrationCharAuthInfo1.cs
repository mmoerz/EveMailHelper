using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class MigrationCharAuthInfo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EveMailRecipient_Character_CharacterId",
                table: "EveMailRecipient");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2022, 12, 1, 20, 49, 0, 354, DateTimeKind.Local).AddTicks(4587), new DateTime(2022, 12, 1, 19, 49, 0, 354, DateTimeKind.Utc).AddTicks(4563) });

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailRecipient_Character_CharacterId",
                table: "EveMailRecipient",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EveMailRecipient_Character_CharacterId",
                table: "EveMailRecipient");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2022, 12, 1, 12, 57, 26, 67, DateTimeKind.Local).AddTicks(8063), new DateTime(2022, 12, 1, 11, 57, 26, 67, DateTimeKind.Utc).AddTicks(8040) });

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailRecipient_Character_CharacterId",
                table: "EveMailRecipient",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
