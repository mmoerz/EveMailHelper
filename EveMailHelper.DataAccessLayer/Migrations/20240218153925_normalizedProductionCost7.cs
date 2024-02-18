using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class normalizedProductionCost7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductPricePerUnit",
                schema: "market",
                table: "NormalizedProductionCost",
                newName: "ProductSellPricePerUnit");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 18, 16, 39, 25, 563, DateTimeKind.Local).AddTicks(6651), new DateTime(2024, 2, 18, 15, 39, 25, 563, DateTimeKind.Utc).AddTicks(6627) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductSellPricePerUnit",
                schema: "market",
                table: "NormalizedProductionCost",
                newName: "ProductPricePerUnit");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 18, 16, 38, 31, 465, DateTimeKind.Local).AddTicks(9532), new DateTime(2024, 2, 18, 15, 38, 31, 465, DateTimeKind.Utc).AddTicks(9502) });
        }
    }
}
