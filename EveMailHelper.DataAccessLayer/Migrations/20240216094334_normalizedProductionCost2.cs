using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class normalizedProductionCost2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Volume",
                schema: "market",
                table: "BuyListItem",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BestPriceSum",
                schema: "market",
                table: "NormalizeProductionCost",
                type: "float",
                nullable: false,
                computedColumnSql: "[BestPriceJobCost] + [BestPriceComponentCost]");

            migrationBuilder.AddColumn<double>(
                name: "DirectCostSum",
                schema: "market",
                table: "NormalizeProductionCost",
                type: "float",
                nullable: false,
                computedColumnSql: "[DirectJobCost] + [DirectComponentCost]");

            migrationBuilder.AddColumn<double>(
                name: "ProductCostSum",
                schema: "market",
                table: "NormalizeProductionCost",
                type: "float",
                nullable: false,
                computedColumnSql: "[ProductQuantity] * [ProductPricePerUnit]");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 16, 10, 43, 34, 452, DateTimeKind.Local).AddTicks(8094), new DateTime(2024, 2, 16, 9, 43, 34, 452, DateTimeKind.Utc).AddTicks(8069) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestPriceSum",
                schema: "market",
                table: "NormalizeProductionCost");

            migrationBuilder.DropColumn(
                name: "DirectCostSum",
                schema: "market",
                table: "NormalizeProductionCost");

            migrationBuilder.DropColumn(
                name: "ProductCostSum",
                schema: "market",
                table: "NormalizeProductionCost");

            migrationBuilder.DropColumn(
                name: "Volume",
                schema: "market",
                table: "BuyListItem");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 14, 10, 38, 26, 866, DateTimeKind.Local).AddTicks(2308), new DateTime(2024, 2, 14, 9, 38, 26, 866, DateTimeKind.Utc).AddTicks(2286) });
        }
    }
}
