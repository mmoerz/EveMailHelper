using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class market2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastUpdated",
                schema: "market",
                table: "MarketOrder",
                newName: "LastUpdatedFromEve");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedFromEve",
                schema: "market",
                table: "MarketPrice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 12, 18, 48, 37, 757, DateTimeKind.Local).AddTicks(2141), new DateTime(2024, 2, 12, 17, 48, 37, 757, DateTimeKind.Utc).AddTicks(2114) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedFromEve",
                schema: "market",
                table: "MarketPrice")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MarketPriceHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "market");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedFromEve",
                schema: "market",
                table: "MarketOrder",
                newName: "lastUpdated");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 12, 18, 36, 52, 726, DateTimeKind.Local).AddTicks(6594), new DateTime(2024, 2, 12, 17, 36, 52, 726, DateTimeKind.Utc).AddTicks(6572) });
        }
    }
}
