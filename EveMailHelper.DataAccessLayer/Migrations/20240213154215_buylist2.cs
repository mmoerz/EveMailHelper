using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class buylist2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                schema: "market",
                table: "BuyListItem",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 13, 16, 42, 14, 602, DateTimeKind.Local).AddTicks(4633), new DateTime(2024, 2, 13, 15, 42, 14, 602, DateTimeKind.Utc).AddTicks(4592) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "market",
                table: "BuyListItem");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 13, 15, 50, 11, 850, DateTimeKind.Local).AddTicks(7286), new DateTime(2024, 2, 13, 14, 50, 11, 850, DateTimeKind.Utc).AddTicks(7263) });
        }
    }
}
