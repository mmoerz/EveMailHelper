using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class marketorder1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "market");

            migrationBuilder.CreateTable(
                name: "MarketOrder",
                schema: "market",
                columns: table => new
                {
                    EveId = table.Column<long>(type: "bigint", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsBuyOrder = table.Column<bool>(type: "bit", nullable: false),
                    Issued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    MinVolume = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Range = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SolarSystemId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    VolumeRemain = table.Column<int>(type: "int", nullable: false),
                    VolumeTotal = table.Column<int>(type: "int", nullable: false),
                    lastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketOrder", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_MarketOrder_EveType_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_MarketOrder_SolarSystem_SolarSystemId",
                        column: x => x.SolarSystemId,
                        principalSchema: "Sde",
                        principalTable: "SolarSystem",
                        principalColumn: "EveId");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MarketOrderHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "market")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 11, 14, 31, 11, 561, DateTimeKind.Local).AddTicks(1009), new DateTime(2024, 2, 11, 13, 31, 11, 561, DateTimeKind.Utc).AddTicks(989) });

            migrationBuilder.CreateIndex(
                name: "IX_MarketOrder_SolarSystemId",
                schema: "market",
                table: "MarketOrder",
                column: "SolarSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketOrder_TypeId",
                schema: "market",
                table: "MarketOrder",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketOrder",
                schema: "market")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MarketOrderHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "market")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 9, 18, 55, 14, 414, DateTimeKind.Local).AddTicks(4372), new DateTime(2024, 2, 9, 17, 55, 14, 414, DateTimeKind.Utc).AddTicks(4350) });
        }
    }
}
