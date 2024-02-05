using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class evesde1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Sde");

            migrationBuilder.CreateTable(
                name: "Graphic",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", nullable: false),
                    SofFactionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GraphicFile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SofHullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SofRaceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graphic", x => x.EveId);
                });

            migrationBuilder.CreateTable(
                name: "Icon",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", nullable: false),
                    IconFile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icon", x => x.EveId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconId = table.Column<int>(type: "int", nullable: true),
                    Published = table.Column<bool>(type: "bit", nullable: true),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_Category_Icon_IconId",
                        column: x => x.IconId,
                        principalSchema: "Sde",
                        principalTable: "Icon",
                        principalColumn: "EveId");
                });

            migrationBuilder.CreateTable(
                name: "ChrRace",
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
                    table.PrimaryKey("PK_ChrRace", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_ChrRace_Icon_IconId",
                        column: x => x.IconId,
                        principalSchema: "Sde",
                        principalTable: "Icon",
                        principalColumn: "EveId");
                });

            migrationBuilder.CreateTable(
                name: "MarketGroup",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    ParentGroupId = table.Column<int>(type: "int", nullable: true),
                    MarketGroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IconId = table.Column<int>(type: "int", nullable: true),
                    HasTypes = table.Column<bool>(type: "bit", nullable: true),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketGroup", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_MarketGroup_Icon_IconId",
                        column: x => x.IconId,
                        principalSchema: "Sde",
                        principalTable: "Icon",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_MarketGroup_MarketGroup_ParentGroupId",
                        column: x => x.ParentGroupId,
                        principalSchema: "Sde",
                        principalTable: "MarketGroup",
                        principalColumn: "EveId");
                });

            migrationBuilder.CreateTable(
                name: "Group",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IconId = table.Column<int>(type: "int", nullable: true),
                    UseBasePrice = table.Column<bool>(type: "bit", nullable: true),
                    Anchored = table.Column<bool>(type: "bit", nullable: true),
                    Anchorable = table.Column<bool>(type: "bit", nullable: true),
                    FittableNonSingleton = table.Column<bool>(type: "bit", nullable: true),
                    Published = table.Column<bool>(type: "bit", nullable: true),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_Group_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Sde",
                        principalTable: "Category",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_Group_Icon_IconId",
                        column: x => x.IconId,
                        principalSchema: "Sde",
                        principalTable: "Icon",
                        principalColumn: "EveId");
                });

            migrationBuilder.CreateTable(
                name: "EveType",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    TypeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Mass = table.Column<double>(type: "float", nullable: true),
                    Volume = table.Column<double>(type: "float", nullable: true),
                    Capacity = table.Column<double>(type: "float", nullable: true),
                    PortionSize = table.Column<int>(type: "int", nullable: true),
                    RaceId = table.Column<int>(type: "int", nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    Published = table.Column<bool>(type: "bit", nullable: true),
                    MarketGroupId = table.Column<int>(type: "int", nullable: true),
                    IconId = table.Column<int>(type: "int", nullable: true),
                    SoundId = table.Column<int>(type: "int", nullable: true),
                    GraphicId = table.Column<int>(type: "int", nullable: true),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EveType", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_EveType_ChrRace_RaceId",
                        column: x => x.RaceId,
                        principalSchema: "Sde",
                        principalTable: "ChrRace",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_EveType_Group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Sde",
                        principalTable: "Group",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_EveType_Icon_IconId",
                        column: x => x.IconId,
                        principalSchema: "Sde",
                        principalTable: "Icon",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_EveType_MarketGroup_MarketGroupId",
                        column: x => x.MarketGroupId,
                        principalSchema: "Sde",
                        principalTable: "MarketGroup",
                        principalColumn: "EveId");
                });

            migrationBuilder.CreateTable(
                name: "IndustryActivity",
                schema: "Sde",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: true),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryActivity", x => new { x.TypeId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_IndustryActivity_EveType_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                });

            migrationBuilder.CreateTable(
                name: "IndustryBlueprint",
                schema: "Sde",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    MaxProductionLimit = table.Column<int>(type: "int", nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryBlueprint", x => x.TypeId);
                    table.ForeignKey(
                        name: "FK_IndustryBlueprint_EveType_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                });

            migrationBuilder.CreateTable(
                name: "IndustryActivityMaterial",
                schema: "Sde",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    MaterialTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryActivityMaterial", x => new { x.TypeId, x.ActivityId, x.MaterialTypeId });
                    table.ForeignKey(
                        name: "FK_IndustryActivityMaterial_EveType_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_IndustryActivityMaterial_EveType_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_IndustryActivityMaterial_IndustryActivity_TypeId_ActivityId",
                        columns: x => new { x.TypeId, x.ActivityId },
                        principalSchema: "Sde",
                        principalTable: "IndustryActivity",
                        principalColumns: new[] { "TypeId", "ActivityId" });
                });

            migrationBuilder.CreateTable(
                name: "IndustryActivityProbability",
                schema: "Sde",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    Probability = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryActivityProbability", x => new { x.TypeId, x.ActivityId, x.ProductTypeId });
                    table.ForeignKey(
                        name: "FK_IndustryActivityProbability_EveType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_IndustryActivityProbability_IndustryActivity_TypeId_ActivityId",
                        columns: x => new { x.TypeId, x.ActivityId },
                        principalSchema: "Sde",
                        principalTable: "IndustryActivity",
                        principalColumns: new[] { "TypeId", "ActivityId" });
                });

            migrationBuilder.CreateTable(
                name: "IndustryActivityProduct",
                schema: "Sde",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryActivityProduct", x => new { x.TypeId, x.ActivityId, x.ProductTypeId });
                    table.ForeignKey(
                        name: "FK_IndustryActivityProduct_EveType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_IndustryActivityProduct_IndustryActivity_TypeId_ActivityId",
                        columns: x => new { x.TypeId, x.ActivityId },
                        principalSchema: "Sde",
                        principalTable: "IndustryActivity",
                        principalColumns: new[] { "TypeId", "ActivityId" });
                });

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 26, 17, 818, DateTimeKind.Local).AddTicks(5903), new DateTime(2024, 2, 5, 13, 26, 17, 818, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.CreateIndex(
                name: "IX_Category_IconId",
                schema: "Sde",
                table: "Category",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_ChrRace_IconId",
                schema: "Sde",
                table: "ChrRace",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_EveType_GroupId",
                schema: "Sde",
                table: "EveType",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EveType_IconId",
                schema: "Sde",
                table: "EveType",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_EveType_MarketGroupId",
                schema: "Sde",
                table: "EveType",
                column: "MarketGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EveType_RaceId",
                schema: "Sde",
                table: "EveType",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_CategoryId",
                schema: "Sde",
                table: "Group",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_IconId",
                schema: "Sde",
                table: "Group",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_IndustryActivity_ActivityId",
                schema: "Sde",
                table: "IndustryActivity",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_IndustryActivityMaterial_ActivityId",
                schema: "Sde",
                table: "IndustryActivityMaterial",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_IndustryActivityMaterial_MaterialTypeId",
                schema: "Sde",
                table: "IndustryActivityMaterial",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IndustryActivityProbability_ProductTypeId",
                schema: "Sde",
                table: "IndustryActivityProbability",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IndustryActivityProduct_ProductTypeId",
                schema: "Sde",
                table: "IndustryActivityProduct",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketGroup_IconId",
                schema: "Sde",
                table: "MarketGroup",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketGroup_ParentGroupId",
                schema: "Sde",
                table: "MarketGroup",
                column: "ParentGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Graphic",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "IndustryActivityMaterial",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "IndustryActivityProbability",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "IndustryActivityProduct",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "IndustryBlueprint",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "IndustryActivity",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "EveType",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "ChrRace",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "Group",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "MarketGroup",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "Icon",
                schema: "Sde");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 1, 25, 14, 26, 8, 254, DateTimeKind.Local).AddTicks(114), new DateTime(2024, 1, 25, 13, 26, 8, 254, DateTimeKind.Utc).AddTicks(90) });
        }
    }
}
