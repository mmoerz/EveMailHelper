using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class marketprice1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketPrice",
                schema: "market",
                columns: table => new
                {
                    EveTypeId = table.Column<int>(type: "int", nullable: false),
                    AdjustedPrice = table.Column<double>(type: "float", nullable: false),
                    AveragePrice = table.Column<double>(type: "float", nullable: false),
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
                    table.PrimaryKey("PK_MarketPrice", x => x.EveTypeId);
                    table.ForeignKey(
                        name: "FK_MarketPrice_EveType_EveTypeId",
                        column: x => x.EveTypeId,
                        principalSchema: "Sde",
                        principalTable: "EveType",
                        principalColumn: "EveId");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MarketPriceHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "market")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 12, 18, 36, 52, 726, DateTimeKind.Local).AddTicks(6594), new DateTime(2024, 2, 12, 17, 36, 52, 726, DateTimeKind.Utc).AddTicks(6572) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketPrice",
                schema: "market")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MarketPriceHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "market")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.UpdateData(
                table: "Corporations",
                keyColumn: "Id",
                keyValue: new Guid("11110000-0000-0000-0000-000011110000"),
                columns: new[] { "DateFounded", "EveLastUpdated" },
                values: new object[] { new DateTime(2024, 2, 11, 14, 31, 11, 561, DateTimeKind.Local).AddTicks(1009), new DateTime(2024, 2, 11, 13, 31, 11, 561, DateTimeKind.Utc).AddTicks(989) });
        }
    }
}
