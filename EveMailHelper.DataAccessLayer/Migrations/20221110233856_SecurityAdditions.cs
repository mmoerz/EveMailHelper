using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class SecurityAdditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Character",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EveAccountId",
                table: "Character",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EveAccount",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EveAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EveAccount_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Security",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                schema: "Security",
                columns: table => new
                {
                    AccountsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => new { x.AccountsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_AccountRole_Account_AccountsId",
                        column: x => x.AccountsId,
                        principalSchema: "Security",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRole",
                schema: "Security",
                columns: table => new
                {
                    PermissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRole", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PermissionRole_Permission_PermissionsId",
                        column: x => x.PermissionsId,
                        principalSchema: "Security",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_AccountId",
                table: "Character",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_EveAccountId",
                table: "Character",
                column: "EveAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RolesId",
                schema: "Security",
                table: "AccountRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_EveAccount_AccountId",
                schema: "Security",
                table: "EveAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_RolesId",
                schema: "Security",
                table: "PermissionRole",
                column: "RolesId");

            migrationBuilder.InsertData(
               schema: "Security",
               table: "Account",
               columns: new string[7] { "Id", "NickName", "FirstName", "LastName", "Email", "Description", "CreatedDate" },
               values: new string[7] {
                    "00000000-0000-0000-0000-000000000000",
                    "Default",
                    "Default",
                    "Default",
                    "Default@default.com",
                    "Default Account",
                    "11.11.2022"
               }
               );

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Account_AccountId",
                table: "Character",
                column: "AccountId",
                principalSchema: "Security",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.InsertData(
               schema: "Security",
               table: "EveAccount",
               columns: new string[5] { "Id", "AccountId", "Name", "Description", "CreatedDate" },
               values: new string[5] {
                    "00000000-0000-0000-0000-000000000000",
                    "00000000-0000-0000-0000-000000000000",
                    "Default",
                    "Default Eve Account",
                    "11.11.2022"
               }
               );

            migrationBuilder.AddForeignKey(
                name: "FK_Character_EveAccount_EveAccountId",
                table: "Character",
                column: "EveAccountId",
                principalSchema: "Security",
                principalTable: "EveAccount",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Account_AccountId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_EveAccount_EveAccountId",
                table: "Character");

            migrationBuilder.DropTable(
                name: "AccountRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "EveAccount",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "PermissionRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Security");

            migrationBuilder.DropIndex(
                name: "IX_Character_AccountId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_EveAccountId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "EveAccountId",
                table: "Character");
        }
    }
}
