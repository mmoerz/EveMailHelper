using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class sdefix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IndustryActivity_ActivityId",
                schema: "Sde",
                table: "IndustryActivity");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 7, 11, 52, 35, 703, DateTimeKind.Local).AddTicks(8672), new DateTime(2024, 2, 7, 10, 52, 35, 703, DateTimeKind.Utc).AddTicks(8650) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 6, 12, 28, 46, 192, DateTimeKind.Local).AddTicks(2282), new DateTime(2024, 2, 6, 11, 28, 46, 192, DateTimeKind.Utc).AddTicks(2261) });

            migrationBuilder.CreateIndex(
                name: "IX_IndustryActivity_ActivityId",
                schema: "Sde",
                table: "IndustryActivity",
                column: "ActivityId");
        }
    }
}
