using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class sdename1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EveType_ChrRace_RaceId",
                schema: "Sde",
                table: "EveType");

            migrationBuilder.DropTable(
                name: "ChrRace",
                schema: "Sde");

            migrationBuilder.CreateTable(
                name: "CharacterRace",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IconId = table.Column<int>(type: "int", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterRace", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_CharacterRace_Icon_IconId",
                        column: x => x.IconId,
                        principalSchema: "Sde",
                        principalTable: "Icon",
                        principalColumn: "EveId");
                });

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 6, 10, 51, 58, 463, DateTimeKind.Local).AddTicks(8719), new DateTime(2024, 2, 6, 9, 51, 58, 463, DateTimeKind.Utc).AddTicks(8697) });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterRace_IconId",
                schema: "Sde",
                table: "CharacterRace",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_EveType_CharacterRace_RaceId",
                schema: "Sde",
                table: "EveType",
                column: "RaceId",
                principalSchema: "Sde",
                principalTable: "CharacterRace",
                principalColumn: "EveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EveType_CharacterRace_RaceId",
                schema: "Sde",
                table: "EveType");

            migrationBuilder.DropTable(
                name: "CharacterRace",
                schema: "Sde");

            migrationBuilder.CreateTable(
                name: "ChrRace",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", nullable: false),
                    IconId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChrRace", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_ChrRace_Icon_IconId",
                        column: x => x.IconId,
                        principalSchema: "Sde",
                        principalTable: "Icon",
                        principalColumn: "EveId");
                });

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 26, 17, 818, DateTimeKind.Local).AddTicks(5903), new DateTime(2024, 2, 5, 13, 26, 17, 818, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.CreateIndex(
                name: "IX_ChrRace_IconId",
                schema: "Sde",
                table: "ChrRace",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_EveType_ChrRace_RaceId",
                schema: "Sde",
                table: "EveType",
                column: "RaceId",
                principalSchema: "Sde",
                principalTable: "ChrRace",
                principalColumn: "EveId");
        }
    }
}
