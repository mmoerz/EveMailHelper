using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class ReferenceFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Character_CreatorId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Corporations_CreatorCorporationId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Corporations_Alliances_AllianceId",
                table: "Corporations");

            migrationBuilder.DropIndex(
                name: "IX_Alliances_CreatorCorporationId",
                table: "Alliances");

            migrationBuilder.DropIndex(
                name: "IX_Alliances_CreatorId",
                table: "Alliances");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Alliances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorCorporationId",
                table: "Alliances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alliances_CreatorCorporationId",
                table: "Alliances",
                column: "CreatorCorporationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alliances_CreatorId",
                table: "Alliances",
                column: "CreatorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alliances_Character_CreatorId",
                table: "Alliances",
                column: "CreatorId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alliances_Corporations_CreatorCorporationId",
                table: "Alliances",
                column: "CreatorCorporationId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Character_CreatorId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Corporations_CreatorCorporationId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Corporations_Alliances_AllianceId",
                table: "Corporations");

            migrationBuilder.DropIndex(
                name: "IX_Alliances_CreatorCorporationId",
                table: "Alliances");

            migrationBuilder.DropIndex(
                name: "IX_Alliances_CreatorId",
                table: "Alliances");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Alliances",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorCorporationId",
                table: "Alliances",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Alliances_CreatorCorporationId",
                table: "Alliances",
                column: "CreatorCorporationId",
                unique: true,
                filter: "[CreatorCorporationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Alliances_CreatorId",
                table: "Alliances",
                column: "CreatorId",
                unique: true,
                filter: "[CreatorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Alliances_Character_CreatorId",
                table: "Alliances",
                column: "CreatorId",
                principalTable: "Character",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alliances_Corporations_CreatorCorporationId",
                table: "Alliances",
                column: "CreatorCorporationId",
                principalTable: "Corporations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Corporations_Alliances_AllianceId",
                table: "Corporations",
                column: "AllianceId",
                principalTable: "Alliances",
                principalColumn: "Id");
        }
    }
}
