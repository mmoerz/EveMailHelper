using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class MigrationCorpAlliance1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EveDeleteInGame",
                table: "Alliances",
                newName: "EveDeletedInGame");

            migrationBuilder.AddColumn<DateTime>(
                name: "EveLastUpdated",
                table: "Corporations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EveLastUpdated",
                table: "Alliances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2022, 11, 29, 22, 12, 40, 482, DateTimeKind.Local).AddTicks(5529), new DateTime(2022, 11, 29, 21, 12, 40, 482, DateTimeKind.Utc).AddTicks(5495) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EveLastUpdated",
                table: "Corporations");

            migrationBuilder.DropColumn(
                name: "EveLastUpdated",
                table: "Alliances");

            migrationBuilder.RenameColumn(
                name: "EveDeletedInGame",
                table: "Alliances",
                newName: "EveDeleteInGame");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                column: "DateFounded",
                value: new DateTime(2022, 11, 27, 1, 12, 58, 108, DateTimeKind.Local).AddTicks(4937));
        }
    }
}
