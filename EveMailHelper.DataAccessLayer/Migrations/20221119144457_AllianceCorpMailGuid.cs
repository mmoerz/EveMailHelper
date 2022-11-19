using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class AllianceCorpMailGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EveMailId",
                table: "EveMails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EveLabelId",
                table: "EveMailLabels",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EveMailId",
                table: "EveMailLabels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Character",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "BloodlineId",
                table: "Character",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CorporationId",
                table: "Character",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FactionId",
                table: "Character",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RecruitmentNote",
                table: "Character",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SecurityStatus",
                table: "Character",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Character",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EveMailRecipient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EveMailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EveMailRecipient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EveMailRecipient_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EveMailRecipient_EveMails_EveMailId",
                        column: x => x.EveMailId,
                        principalTable: "EveMails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Alliances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatorCorporationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateFounded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutorCorporationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alliances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alliances_Character_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Character",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Corporations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AllianceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CeoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateFounded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    FactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HomeStationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MemberCount = table.Column<int>(type: "int", nullable: false),
                    Shares = table.Column<long>(type: "bigint", nullable: true),
                    TaxRate = table.Column<float>(type: "real", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WarEligible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Corporations_Alliances_AllianceId",
                        column: x => x.AllianceId,
                        principalTable: "Alliances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Corporations_Character_CeoId",
                        column: x => x.CeoId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Corporations_Character_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Character",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EveMailLabels_EveMailId",
                table: "EveMailLabels",
                column: "EveMailId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_CorporationId",
                table: "Character",
                column: "CorporationId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Alliances_ExecutorCorporationId",
                table: "Alliances",
                column: "ExecutorCorporationId",
                unique: true,
                filter: "[ExecutorCorporationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Corporations_AllianceId",
                table: "Corporations",
                column: "AllianceId");

            migrationBuilder.CreateIndex(
                name: "IX_Corporations_CeoId",
                table: "Corporations",
                column: "CeoId",
                unique: true,
                filter: "[CeoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Corporations_CreatorId",
                table: "Corporations",
                column: "CreatorId",
                unique: true,
                filter: "[CreatorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EveMailRecipient_CharacterId",
                table: "EveMailRecipient",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EveMailRecipient_EveMailId",
                table: "EveMailRecipient",
                column: "EveMailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Corporations_CorporationId",
                table: "Character",
                column: "CorporationId",
                principalTable: "Corporations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EveMailLabels_EveMails_EveMailId",
                table: "EveMailLabels",
                column: "EveMailId",
                principalTable: "EveMails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alliances_Corporations_CreatorCorporationId",
                table: "Alliances",
                column: "CreatorCorporationId",
                principalTable: "Corporations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Alliances_Corporations_ExecutorCorporationId",
                table: "Alliances",
                column: "ExecutorCorporationId",
                principalTable: "Corporations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Corporations_CorporationId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_EveMailLabels_EveMails_EveMailId",
                table: "EveMailLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Corporations_CreatorCorporationId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Corporations_ExecutorCorporationId",
                table: "Alliances");

            migrationBuilder.DropTable(
                name: "EveMailRecipient");

            migrationBuilder.DropTable(
                name: "Corporations");

            migrationBuilder.DropTable(
                name: "Alliances");

            migrationBuilder.DropIndex(
                name: "IX_EveMailLabels_EveMailId",
                table: "EveMailLabels");

            migrationBuilder.DropIndex(
                name: "IX_Character_CorporationId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "EveMailId",
                table: "EveMails");

            migrationBuilder.DropColumn(
                name: "EveMailId",
                table: "EveMailLabels");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "BloodlineId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "FactionId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "RecruitmentNote",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "SecurityStatus",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Character");

            migrationBuilder.AlterColumn<int>(
                name: "EveLabelId",
                table: "EveMailLabels",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
