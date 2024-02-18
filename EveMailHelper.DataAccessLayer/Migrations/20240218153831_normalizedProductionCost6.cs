using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class normalizedProductionCost6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestPriceSum",
                schema: "market",
                table: "NormalizedProductionCost")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "NormalizedProductionCostHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "market");

            migrationBuilder.DropColumn(
                name: "DirectCostSum",
                schema: "market",
                table: "NormalizedProductionCost")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "NormalizedProductionCostHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "market");

            migrationBuilder.DropColumn(
                name: "ProductCostSum",
                schema: "market",
                table: "NormalizedProductionCost")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "NormalizedProductionCostHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "market");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 18, 16, 38, 31, 465, DateTimeKind.Local).AddTicks(9532), new DateTime(2024, 2, 18, 15, 38, 31, 465, DateTimeKind.Utc).AddTicks(9502) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BestPriceSum",
                schema: "market",
                table: "NormalizedProductionCost",
                type: "float",
                nullable: false,
                computedColumnSql: "[BestPriceJobCost] + [BestPriceComponentCost]");

            migrationBuilder.AddColumn<double>(
                name: "DirectCostSum",
                schema: "market",
                table: "NormalizedProductionCost",
                type: "float",
                nullable: false,
                computedColumnSql: "[DirectJobCost] + [DirectComponentCost]");

            migrationBuilder.AddColumn<double>(
                name: "ProductCostSum",
                schema: "market",
                table: "NormalizedProductionCost",
                type: "float",
                nullable: false,
                computedColumnSql: "[ProductQuantity] * [ProductPricePerUnit] * [NumberOfRuns]");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 17, 0, 33, 6, 975, DateTimeKind.Local).AddTicks(3815), new DateTime(2024, 2, 16, 23, 33, 6, 975, DateTimeKind.Utc).AddTicks(3791) });
        }
    }
}
