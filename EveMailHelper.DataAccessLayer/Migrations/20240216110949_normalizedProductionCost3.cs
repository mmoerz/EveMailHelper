using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class normalizedProductionCost3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NormalizeProductionCost",
                schema: "market");

            migrationBuilder.CreateTable(
                name: "NormalizedProductionCost",
                schema: "market",
                columns: table => new
                {
                    EveTypeId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    NumberOfRuns = table.Column<int>(type: "int", nullable: false),
                    DirectJobCost = table.Column<double>(type: "float", nullable: false),
                    DirectComponentCost = table.Column<double>(type: "float", nullable: false),
                    DirectCostSum = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[DirectJobCost] + [DirectComponentCost]"),
                    BestPriceJobCost = table.Column<double>(type: "float", nullable: false),
                    BestPriceComponentCost = table.Column<double>(type: "float", nullable: false),
                    BestPriceSum = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[BestPriceJobCost] + [BestPriceComponentCost]"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductPricePerUnit = table.Column<double>(type: "float", nullable: false),
                    ProductCostSum = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[ProductQuantity] * [ProductPricePerUnit]"),
                    LastUpdatedFromEve = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NormalizedProductionCost", x => new { x.EveTypeId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_NormalizedProductionCost_EveType_EveTypeId",
                        column: x => x.EveTypeId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_NormalizedProductionCost_EveType_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_NormalizedProductionCost_IndustryActivity_EveTypeId_ActivityId",
                        columns: x => new { x.EveTypeId, x.ActivityId },
                        principalSchema: "Sde",
                        principalTable: "IndustryActivity",
                        principalColumns: new[] { "TypeId", "ActivityId" });
                });

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 16, 12, 9, 49, 159, DateTimeKind.Local).AddTicks(3999), new DateTime(2024, 2, 16, 11, 9, 49, 159, DateTimeKind.Utc).AddTicks(3978) });

            migrationBuilder.CreateIndex(
                name: "IX_NormalizedProductionCost_ProductId",
                schema: "market",
                table: "NormalizedProductionCost",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NormalizedProductionCost",
                schema: "market");

            migrationBuilder.CreateTable(
                name: "NormalizeProductionCost",
                schema: "market",
                columns: table => new
                {
                    EveTypeId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    BestPriceComponentCost = table.Column<double>(type: "float", nullable: false),
                    BestPriceJobCost = table.Column<double>(type: "float", nullable: false),
                    BestPriceSum = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[BestPriceJobCost] + [BestPriceComponentCost]"),
                    DirectComponentCost = table.Column<double>(type: "float", nullable: false),
                    DirectCostSum = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[DirectJobCost] + [DirectComponentCost]"),
                    DirectJobCost = table.Column<double>(type: "float", nullable: false),
                    LastUpdatedFromEve = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfRuns = table.Column<int>(type: "int", nullable: false),
                    ProductCostSum = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[ProductQuantity] * [ProductPricePerUnit]"),
                    ProductPricePerUnit = table.Column<double>(type: "float", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 16, 10, 43, 34, 452, DateTimeKind.Local).AddTicks(8094), new DateTime(2024, 2, 16, 9, 43, 34, 452, DateTimeKind.Utc).AddTicks(8069) });

            migrationBuilder.CreateIndex(
                name: "IX_NormalizeProductionCost_ProductId",
                schema: "market",
                table: "NormalizeProductionCost",
                column: "ProductId");
        }
    }
}
