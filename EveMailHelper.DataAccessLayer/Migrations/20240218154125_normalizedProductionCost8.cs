using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class normalizedProductionCost8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ProductBuyPricePerUnit",
                schema: "market",
                table: "NormalizedProductionCost",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 18, 16, 41, 25, 153, DateTimeKind.Local).AddTicks(4950), new DateTime(2024, 2, 18, 15, 41, 25, 153, DateTimeKind.Utc).AddTicks(4926) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductBuyPricePerUnit",
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
                values: new object[] { new DateTime(2024, 2, 18, 16, 39, 25, 563, DateTimeKind.Local).AddTicks(6651), new DateTime(2024, 2, 18, 15, 39, 25, 563, DateTimeKind.Utc).AddTicks(6627) });
        }
    }
}
