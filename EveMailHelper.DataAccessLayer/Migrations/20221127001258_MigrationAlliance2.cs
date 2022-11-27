using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class MigrationAlliance2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Corporations",
                columns: new[] { "Id", "AllianceId", "CeoId", "CreatorId", "DateFounded", "Description", "EveDeletedInGame", "EveId", "FactionId", "HomeStationId", "MemberCount", "Name", "Shares", "TaxRate", "Ticker", "Url", "WarEligible" },
                values: new object[] { new Guid("11110000-0000-0000-0000-000011110000"), null, null, null, new DateTime(2022, 11, 27, 1, 12, 58, 108, DateTimeKind.Local).AddTicks(4937), "Noname Default", false, 0, null, null, 0, "Noname Default", null, 0f, "Noname Default", null, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"));
        }
    }
}
