using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class buylistAndProdCosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyList",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NormalizeProductionCost",
                schema: "market",
                columns: table => new
                {
                    EveTypeId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    TotalJobCost = table.Column<double>(type: "float", nullable: false),
                    TotalComponentCost = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductSellPrice = table.Column<double>(type: "float", nullable: false),
                    LastUpdatedFromEve = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NormalizeProductionCost", x => new { x.EveTypeId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_NormalizeProductionCost_EveType_EveTypeId",
                        column: x => x.EveTypeId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_NormalizeProductionCost_EveType_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_NormalizeProductionCost_IndustryActivity_EveTypeId_ActivityId",
                        columns: x => new { x.EveTypeId, x.ActivityId },
                        principalSchema: "Sde",
                        principalTable: "IndustryActivity",
                        principalColumns: new[] { "TypeId", "ActivityId" });
                });

            migrationBuilder.CreateTable(
                name: "BuyListItem",
                schema: "market",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyListItem_BuyList_BuyListId",
                        column: x => x.BuyListId,
                        principalSchema: "market",
                        principalTable: "BuyList",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BuyListItem_EveType_EveTypeId",
                        column: x => x.EveTypeId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                });

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 13, 15, 50, 11, 850, DateTimeKind.Local).AddTicks(7286), new DateTime(2024, 2, 13, 14, 50, 11, 850, DateTimeKind.Utc).AddTicks(7263) });

            migrationBuilder.CreateIndex(
                name: "IX_BuyListItem_BuyListId",
                schema: "market",
                table: "BuyListItem",
                column: "BuyListId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyListItem_EveTypeId",
                schema: "market",
                table: "BuyListItem",
                column: "EveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NormalizeProductionCost_ProductId",
                schema: "market",
                table: "NormalizeProductionCost",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyListItem",
                schema: "market");

            migrationBuilder.DropTable(
                name: "NormalizeProductionCost",
                schema: "market");

            migrationBuilder.DropTable(
                name: "BuyList",
                schema: "market");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 13, 1, 34, 29, 454, DateTimeKind.Local).AddTicks(8130), new DateTime(2024, 2, 13, 0, 34, 29, 454, DateTimeKind.Utc).AddTicks(8107) });
        }
    }
}
