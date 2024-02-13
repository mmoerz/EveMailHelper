using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class buylist3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 13, 22, 40, 45, 215, DateTimeKind.Local).AddTicks(8358), new DateTime(2024, 2, 13, 21, 40, 45, 215, DateTimeKind.Utc).AddTicks(8329) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 13, 16, 42, 14, 602, DateTimeKind.Local).AddTicks(4633), new DateTime(2024, 2, 13, 15, 42, 14, 602, DateTimeKind.Utc).AddTicks(4592) });
        }
    }
}
