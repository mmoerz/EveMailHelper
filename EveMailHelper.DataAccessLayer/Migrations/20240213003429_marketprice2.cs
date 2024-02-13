using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class marketprice2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketPrice_EveType_EveTypeId",
                schema: "market",
                table: "MarketPrice");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 13, 1, 34, 29, 454, DateTimeKind.Local).AddTicks(8130), new DateTime(2024, 2, 13, 0, 34, 29, 454, DateTimeKind.Utc).AddTicks(8107) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 12, 18, 48, 37, 757, DateTimeKind.Local).AddTicks(2141), new DateTime(2024, 2, 12, 17, 48, 37, 757, DateTimeKind.Utc).AddTicks(2114) });

            migrationBuilder.AddForeignKey(
                name: "FK_MarketPrice_EveType_EveTypeId",
                schema: "market",
                table: "MarketPrice",
                column: "EveTypeId",
                principalSchema: "Sde",
                principalTable: "EveType",
                principalColumn: "EveId");
        }
    }
}
