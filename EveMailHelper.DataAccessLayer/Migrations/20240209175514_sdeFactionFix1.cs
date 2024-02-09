using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class sdeFactionFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Faction_SolarSystemId",
                schema: "Sde",
                table: "Faction");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 9, 18, 55, 14, 414, DateTimeKind.Local).AddTicks(4372), new DateTime(2024, 2, 9, 17, 55, 14, 414, DateTimeKind.Utc).AddTicks(4350) });

            migrationBuilder.CreateIndex(
                name: "IX_Faction_SolarSystemId",
                schema: "Sde",
                table: "Faction",
                column: "SolarSystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Faction_SolarSystemId",
                schema: "Sde",
                table: "Faction");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 9, 18, 50, 46, 740, DateTimeKind.Local).AddTicks(2836), new DateTime(2024, 2, 9, 17, 50, 46, 740, DateTimeKind.Utc).AddTicks(2815) });

            migrationBuilder.CreateIndex(
                name: "IX_Faction_SolarSystemId",
                schema: "Sde",
                table: "Faction",
                column: "SolarSystemId",
                unique: true);
        }
    }
}
