using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class MigrationCorpAlliance2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Corporations_CreatorCorporationId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Corporations_ExecutorCorporationId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Corporations_Alliances_AllianceId",
                table: "Corporations");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2022, 11, 29, 22, 51, 33, 126, DateTimeKind.Local).AddTicks(2925), new DateTime(2022, 11, 29, 21, 51, 33, 126, DateTimeKind.Utc).AddTicks(2902) });

            migrationBuilder.AddForeignKey(
                name: "FK_Alliances_Corporations_CreatorCorporationId",
                table: "Alliances",
                column: "CreatorCorporationId",
                principalTable: "Corporations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alliances_Corporations_ExecutorCorporationId",
                table: "Alliances",
                column: "ExecutorCorporationId",
                principalTable: "Corporations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Corporations_Alliances_AllianceId",
                table: "Corporations",
                column: "AllianceId",
                principalTable: "Alliances",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Corporations_CreatorCorporationId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Corporations_ExecutorCorporationId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Corporations_Alliances_AllianceId",
                table: "Corporations");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2022, 11, 29, 22, 12, 40, 482, DateTimeKind.Local).AddTicks(5529), new DateTime(2022, 11, 29, 21, 12, 40, 482, DateTimeKind.Utc).AddTicks(5495) });

            migrationBuilder.AddForeignKey(
                name: "FK_Alliances_Corporations_CreatorCorporationId",
                table: "Alliances",
                column: "CreatorCorporationId",
                principalTable: "Corporations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alliances_Corporations_ExecutorCorporationId",
                table: "Alliances",
                column: "ExecutorCorporationId",
                principalTable: "Corporations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Corporations_Alliances_AllianceId",
                table: "Corporations",
                column: "AllianceId",
                principalTable: "Alliances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
