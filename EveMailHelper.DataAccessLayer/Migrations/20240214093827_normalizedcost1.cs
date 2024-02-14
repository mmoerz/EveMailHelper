using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class normalizedcost1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalJobCost",
                schema: "market",
                table: "NormalizeProductionCost",
                newName: "ProductPricePerUnit");

            migrationBuilder.RenameColumn(
                name: "TotalComponentCost",
                schema: "market",
                table: "NormalizeProductionCost",
                newName: "DirectJobCost");

            migrationBuilder.RenameColumn(
                name: "ProductSellPrice",
                schema: "market",
                table: "NormalizeProductionCost",
                newName: "DirectComponentCost");

            migrationBuilder.AddColumn<double>(
                name: "BestPriceComponentCost",
                schema: "market",
                table: "NormalizeProductionCost",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BestPriceJobCost",
                schema: "market",
                table: "NormalizeProductionCost",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRuns",
                schema: "market",
                table: "NormalizeProductionCost",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 14, 10, 38, 26, 866, DateTimeKind.Local).AddTicks(2308), new DateTime(2024, 2, 14, 9, 38, 26, 866, DateTimeKind.Utc).AddTicks(2286) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestPriceComponentCost",
                schema: "market",
                table: "NormalizeProductionCost");

            migrationBuilder.DropColumn(
                name: "BestPriceJobCost",
                schema: "market",
                table: "NormalizeProductionCost");

            migrationBuilder.DropColumn(
                name: "NumberOfRuns",
                schema: "market",
                table: "NormalizeProductionCost");

            migrationBuilder.RenameColumn(
                name: "ProductPricePerUnit",
                schema: "market",
                table: "NormalizeProductionCost",
                newName: "TotalJobCost");

            migrationBuilder.RenameColumn(
                name: "DirectJobCost",
                schema: "market",
                table: "NormalizeProductionCost",
                newName: "TotalComponentCost");

            migrationBuilder.RenameColumn(
                name: "DirectComponentCost",
                schema: "market",
                table: "NormalizeProductionCost",
                newName: "ProductSellPrice");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 13, 22, 40, 45, 215, DateTimeKind.Local).AddTicks(8358), new DateTime(2024, 2, 13, 21, 40, 45, 215, DateTimeKind.Utc).AddTicks(8329) });
        }
    }
}
