using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class normalizedProductionCost4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ProductCostSum",
                schema: "market",
                table: "NormalizedProductionCost",
                type: "float",
                nullable: false,
                computedColumnSql: "[ProductQuantity] * [ProductPricePerUnit] * [NumberOfRuns]",
                oldClrType: typeof(double),
                oldType: "float",
                oldComputedColumnSql: "[ProductQuantity] * [ProductPricePerUnit]");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 16, 22, 49, 32, 626, DateTimeKind.Local).AddTicks(8095), new DateTime(2024, 2, 16, 21, 49, 32, 626, DateTimeKind.Utc).AddTicks(8067) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ProductCostSum",
                schema: "market",
                table: "NormalizedProductionCost",
                type: "float",
                nullable: false,
                computedColumnSql: "[ProductQuantity] * [ProductPricePerUnit]",
                oldClrType: typeof(double),
                oldType: "float",
                oldComputedColumnSql: "[ProductQuantity] * [ProductPricePerUnit] * [NumberOfRuns]");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 16, 12, 9, 49, 159, DateTimeKind.Local).AddTicks(3999), new DateTime(2024, 2, 16, 11, 9, 49, 159, DateTimeKind.Utc).AddTicks(3978) });
        }
    }
}
