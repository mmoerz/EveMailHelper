using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.EnsureSchema(
                name: "Sde");

            migrationBuilder.EnsureSchema(
                name: "Eve");

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
                name: "ChatFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EveMailTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EveMailTemplate", x => x.Id);
                });

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
                name: "MailLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailLists", x => x.Id);
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
                name: "Race",
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
                    table.PrimaryKey("PK_Race", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_Race_Icon_IconId",
                        column: x => x.IconId,
                        principalSchema: "Sde",
                        principalTable: "Icon",
                        principalColumn: "EveId");
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
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
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
                    table.ForeignKey(
                        name: "FK_EveType_Race_RaceId",
                        column: x => x.RaceId,
                        principalSchema: "Sde",
                        principalTable: "Race",
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

            migrationBuilder.CreateTable(
                name: "Alliances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatorCorporationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateFounded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutorCorporationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alliances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveId = table.Column<int>(type: "int", nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReallifeAge = table.Column<int>(type: "int", nullable: true),
                    BloodlineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorporationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: true),
                    FactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    RecruitmentNote = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    SecurityStatus = table.Column<float>(type: "real", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsExcluded = table.Column<bool>(type: "bit", nullable: false),
                    IsInRecruitment = table.Column<bool>(type: "bit", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "None"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Security",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Character_EveAccount_EveAccountId",
                        column: x => x.EveAccountId,
                        principalSchema: "Security",
                        principalTable: "EveAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacterAuthInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "random number for added security"),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CharId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccessToken = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "oauth accesstoken"),
                    RefreshToken = table.Column<string>(type: "nvarchar(1536)", maxLength: 1536, nullable: false, comment: "oauth refreshtoken"),
                    TokenType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "??"),
                    Scopes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAuthInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterAuthInfos_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Security",
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacterAuthInfos_Character_CharId",
                        column: x => x.CharId,
                        principalTable: "Character",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChannelName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AttachedToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListenerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionStarted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Character_AttachedToId",
                        column: x => x.AttachedToId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chats_Character_ListenerId",
                        column: x => x.ListenerId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chats_ChatFiles_ChatFileId",
                        column: x => x.ChatFileId,
                        principalTable: "ChatFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false),
                    FactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HomeStationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MemberCount = table.Column<int>(type: "int", nullable: false),
                    Shares = table.Column<long>(type: "bigint", nullable: true),
                    TaxRate = table.Column<float>(type: "real", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WarEligible = table.Column<bool>(type: "bit", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Mail",
                schema: "Eve",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveId = table.Column<long>(type: "bigint", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 16000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveMailTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mail_Character_FromId",
                        column: x => x.FromId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mail_Character_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mail_EveMailTemplate_EveMailTemplateId",
                        column: x => x.EveMailTemplateId,
                        principalTable: "EveMailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttachedToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Character_AttachedToId",
                        column: x => x.AttachedToId,
                        principalTable: "Character",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Character_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EveMailSentTo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveMailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EveMailSentTo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EveMailSentTo_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EveMailSentTo_Mail_EveMailId",
                        column: x => x.EveMailId,
                        principalSchema: "Eve",
                        principalTable: "Mail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MailLabels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EveLabelId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnreadCount = table.Column<int>(type: "int", nullable: true),
                    MailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailLabels_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MailLabels_Mail_MailId",
                        column: x => x.MailId,
                        principalSchema: "Eve",
                        principalTable: "Mail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MailRecipients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailRecipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailRecipients_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MailRecipients_Mail_MailId",
                        column: x => x.MailId,
                        principalSchema: "Eve",
                        principalTable: "Mail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Constellation",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    Z = table.Column<double>(type: "float", nullable: false),
                    XMin = table.Column<double>(type: "float", nullable: false),
                    XMax = table.Column<double>(type: "float", nullable: false),
                    YMin = table.Column<double>(type: "float", nullable: false),
                    YMax = table.Column<double>(type: "float", nullable: false),
                    ZMin = table.Column<double>(type: "float", nullable: false),
                    ZMax = table.Column<double>(type: "float", nullable: false),
                    FactionId = table.Column<int>(type: "int", nullable: true),
                    Radius = table.Column<double>(type: "float", nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constellation", x => x.EveId);
                });

            migrationBuilder.CreateTable(
                name: "Faction",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    SolarSystemId = table.Column<int>(type: "int", nullable: false),
                    CorporationId = table.Column<int>(type: "int", nullable: true),
                    SizeFactor = table.Column<double>(type: "float", nullable: false),
                    MilitiaCorporationId = table.Column<int>(type: "int", nullable: true),
                    IconId = table.Column<int>(type: "int", nullable: false),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faction", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_Faction_Icon_IconId",
                        column: x => x.IconId,
                        principalSchema: "Sde",
                        principalTable: "Icon",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_Faction_Race_RaceId",
                        column: x => x.RaceId,
                        principalSchema: "Sde",
                        principalTable: "Race",
                        principalColumn: "EveId");
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    Z = table.Column<double>(type: "float", nullable: false),
                    XMin = table.Column<double>(type: "float", nullable: false),
                    XMax = table.Column<double>(type: "float", nullable: false),
                    YMin = table.Column<double>(type: "float", nullable: false),
                    YMax = table.Column<double>(type: "float", nullable: false),
                    ZMin = table.Column<double>(type: "float", nullable: false),
                    ZMax = table.Column<double>(type: "float", nullable: false),
                    FactionId = table.Column<int>(type: "int", nullable: true),
                    Nebula = table.Column<int>(type: "int", nullable: false),
                    Radius = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_Region_Faction_FactionId",
                        column: x => x.FactionId,
                        principalSchema: "Sde",
                        principalTable: "Faction",
                        principalColumn: "EveId");
                });

            migrationBuilder.CreateTable(
                name: "SolarSystem",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    ConstellationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    Z = table.Column<double>(type: "float", nullable: false),
                    XMin = table.Column<double>(type: "float", nullable: false),
                    XMax = table.Column<double>(type: "float", nullable: false),
                    YMin = table.Column<double>(type: "float", nullable: false),
                    YMax = table.Column<double>(type: "float", nullable: false),
                    ZMin = table.Column<double>(type: "float", nullable: false),
                    ZMax = table.Column<double>(type: "float", nullable: false),
                    Luminosity = table.Column<double>(type: "float", nullable: false),
                    Border = table.Column<bool>(type: "bit", nullable: false),
                    Fringe = table.Column<bool>(type: "bit", nullable: false),
                    Corridor = table.Column<bool>(type: "bit", nullable: false),
                    Hub = table.Column<bool>(type: "bit", nullable: false),
                    International = table.Column<bool>(type: "bit", nullable: false),
                    Regional = table.Column<bool>(type: "bit", nullable: false),
                    Security = table.Column<double>(type: "float", nullable: false),
                    FactionId = table.Column<int>(type: "int", nullable: true),
                    Radius = table.Column<double>(type: "float", nullable: false),
                    SunTypeId = table.Column<int>(type: "int", nullable: true),
                    SecurityClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolarSystem", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_SolarSystem_Constellation_ConstellationId",
                        column: x => x.ConstellationId,
                        principalSchema: "Sde",
                        principalTable: "Constellation",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_SolarSystem_Faction_FactionId",
                        column: x => x.FactionId,
                        principalSchema: "Sde",
                        principalTable: "Faction",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_SolarSystem_Region_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "Sde",
                        principalTable: "Region",
                        principalColumn: "EveId");
                });

            migrationBuilder.CreateTable(
                name: "NpcCorporation",
                schema: "Sde",
                columns: table => new
                {
                    EveId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SolarSystemId = table.Column<int>(type: "int", nullable: true),
                    FriendId = table.Column<int>(type: "int", nullable: true),
                    EnemyId = table.Column<int>(type: "int", nullable: true),
                    PublicShares = table.Column<int>(type: "int", nullable: false),
                    InitialPrice = table.Column<int>(type: "int", nullable: false),
                    MinSecurity = table.Column<double>(type: "float", nullable: false),
                    FactionId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: false),
                    IconId = table.Column<int>(type: "int", nullable: true),
                    EveLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EveDeletedInGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NpcCorporation", x => x.EveId);
                    table.ForeignKey(
                        name: "FK_NpcCorporation_Faction_FactionId",
                        column: x => x.FactionId,
                        principalSchema: "Sde",
                        principalTable: "Faction",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_NpcCorporation_Icon_IconId",
                        column: x => x.IconId,
                        principalSchema: "Sde",
                        principalTable: "Icon",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_NpcCorporation_NpcCorporation_EnemyId",
                        column: x => x.EnemyId,
                        principalSchema: "Sde",
                        principalTable: "NpcCorporation",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_NpcCorporation_NpcCorporation_FriendId",
                        column: x => x.FriendId,
                        principalSchema: "Sde",
                        principalTable: "NpcCorporation",
                        principalColumn: "EveId");
                    table.ForeignKey(
                        name: "FK_NpcCorporation_SolarSystem_SolarSystemId",
                        column: x => x.SolarSystemId,
                        principalSchema: "Sde",
                        principalTable: "SolarSystem",
                        principalColumn: "EveId");
                });

            migrationBuilder.InsertData(
                table: "Corporations",
                columns: new[] { "Id", "AllianceId", "CeoId", "CreatorId", "DateFounded", "Description", "EveDeletedInGame", "EveId", "EveLastUpdated", "FactionId", "HomeStationId", "MemberCount", "Name", "Shares", "TaxRate", "Ticker", "Url", "WarEligible" },
                values: new object[] { new Guid("11110000-0000-0000-0000-000011110000"), null, null, null, new DateTime(2024, 2, 9, 18, 50, 46, 740, DateTimeKind.Local).AddTicks(2836), "Noname Default", false, 0, new DateTime(2024, 2, 9, 17, 50, 46, 740, DateTimeKind.Utc).AddTicks(2815), null, null, 0, "Noname Default", null, 0f, "Noname Default", null, false });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RolesId",
                schema: "Security",
                table: "AccountRole",
                column: "RolesId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Alliances_ExecutorCorporationId",
                table: "Alliances",
                column: "ExecutorCorporationId",
                unique: true,
                filter: "[ExecutorCorporationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Category_IconId",
                schema: "Sde",
                table: "Category",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_AccountId",
                table: "Character",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_CorporationId",
                table: "Character",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_EveAccountId",
                table: "Character",
                column: "EveAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAuthInfos_AccountId",
                table: "CharacterAuthInfos",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAuthInfos_CharId",
                table: "CharacterAuthInfos",
                column: "CharId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_AuthorId",
                table: "ChatMessages",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatId",
                table: "ChatMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_AttachedToId",
                table: "Chats",
                column: "AttachedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ChatFileId",
                table: "Chats",
                column: "ChatFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ListenerId",
                table: "Chats",
                column: "ListenerId");

            migrationBuilder.CreateIndex(
                name: "IX_Constellation_FactionId",
                schema: "Sde",
                table: "Constellation",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Constellation_RegionId",
                schema: "Sde",
                table: "Constellation",
                column: "RegionId");

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
                name: "IX_EveAccount_AccountId",
                schema: "Security",
                table: "EveAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_EveMailSentTo_CharacterId",
                table: "EveMailSentTo",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EveMailSentTo_EveMailId",
                table: "EveMailSentTo",
                column: "EveMailId");

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
                name: "IX_Faction_CorporationId",
                schema: "Sde",
                table: "Faction",
                column: "CorporationId",
                unique: true,
                filter: "[CorporationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Faction_IconId",
                schema: "Sde",
                table: "Faction",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Faction_MilitiaCorporationId",
                schema: "Sde",
                table: "Faction",
                column: "MilitiaCorporationId",
                unique: true,
                filter: "[MilitiaCorporationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Faction_RaceId",
                schema: "Sde",
                table: "Faction",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Faction_SolarSystemId",
                schema: "Sde",
                table: "Faction",
                column: "SolarSystemId",
                unique: true);

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
                name: "IX_Mail_EveMailTemplateId",
                schema: "Eve",
                table: "Mail",
                column: "EveMailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Mail_FromId",
                schema: "Eve",
                table: "Mail",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Mail_OwnerId",
                schema: "Eve",
                table: "Mail",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MailLabels_CharacterId",
                table: "MailLabels",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_MailLabels_MailId",
                table: "MailLabels",
                column: "MailId");

            migrationBuilder.CreateIndex(
                name: "IX_MailRecipients_CharacterId",
                table: "MailRecipients",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_MailRecipients_MailId",
                table: "MailRecipients",
                column: "MailId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Notes_AttachedToId",
                table: "Notes",
                column: "AttachedToId");

            migrationBuilder.CreateIndex(
                name: "IX_NpcCorporation_EnemyId",
                schema: "Sde",
                table: "NpcCorporation",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_NpcCorporation_FactionId",
                schema: "Sde",
                table: "NpcCorporation",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_NpcCorporation_FriendId",
                schema: "Sde",
                table: "NpcCorporation",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_NpcCorporation_IconId",
                schema: "Sde",
                table: "NpcCorporation",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_NpcCorporation_SolarSystemId",
                schema: "Sde",
                table: "NpcCorporation",
                column: "SolarSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_RolesId",
                schema: "Security",
                table: "PermissionRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Race_IconId",
                schema: "Sde",
                table: "Race",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_FactionId",
                schema: "Sde",
                table: "Region",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystem_ConstellationId",
                schema: "Sde",
                table: "SolarSystem",
                column: "ConstellationId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystem_FactionId",
                schema: "Sde",
                table: "SolarSystem",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystem_RegionId",
                schema: "Sde",
                table: "SolarSystem",
                column: "RegionId");

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alliances_Corporations_ExecutorCorporationId",
                table: "Alliances",
                column: "ExecutorCorporationId",
                principalTable: "Corporations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Corporations_CorporationId",
                table: "Character",
                column: "CorporationId",
                principalTable: "Corporations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Constellation_Faction_FactionId",
                schema: "Sde",
                table: "Constellation",
                column: "FactionId",
                principalSchema: "Sde",
                principalTable: "Faction",
                principalColumn: "EveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Constellation_Region_RegionId",
                schema: "Sde",
                table: "Constellation",
                column: "RegionId",
                principalSchema: "Sde",
                principalTable: "Region",
                principalColumn: "EveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faction_NpcCorporation_CorporationId",
                schema: "Sde",
                table: "Faction",
                column: "CorporationId",
                principalSchema: "Sde",
                principalTable: "NpcCorporation",
                principalColumn: "EveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faction_NpcCorporation_MilitiaCorporationId",
                schema: "Sde",
                table: "Faction",
                column: "MilitiaCorporationId",
                principalSchema: "Sde",
                principalTable: "NpcCorporation",
                principalColumn: "EveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faction_SolarSystem_SolarSystemId",
                schema: "Sde",
                table: "Faction",
                column: "SolarSystemId",
                principalSchema: "Sde",
                principalTable: "SolarSystem",
                principalColumn: "EveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Account_AccountId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_EveAccount_Account_AccountId",
                schema: "Security",
                table: "EveAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Character_CreatorId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Corporations_Character_CeoId",
                table: "Corporations");

            migrationBuilder.DropForeignKey(
                name: "FK_Corporations_Character_CreatorId",
                table: "Corporations");

            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Corporations_CreatorCorporationId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Alliances_Corporations_ExecutorCorporationId",
                table: "Alliances");

            migrationBuilder.DropForeignKey(
                name: "FK_Faction_Icon_IconId",
                schema: "Sde",
                table: "Faction");

            migrationBuilder.DropForeignKey(
                name: "FK_NpcCorporation_Icon_IconId",
                schema: "Sde",
                table: "NpcCorporation");

            migrationBuilder.DropForeignKey(
                name: "FK_Race_Icon_IconId",
                schema: "Sde",
                table: "Race");

            migrationBuilder.DropForeignKey(
                name: "FK_Constellation_Faction_FactionId",
                schema: "Sde",
                table: "Constellation");

            migrationBuilder.DropForeignKey(
                name: "FK_NpcCorporation_Faction_FactionId",
                schema: "Sde",
                table: "NpcCorporation");

            migrationBuilder.DropForeignKey(
                name: "FK_Region_Faction_FactionId",
                schema: "Sde",
                table: "Region");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystem_Faction_FactionId",
                schema: "Sde",
                table: "SolarSystem");

            migrationBuilder.DropTable(
                name: "AccountRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "CharacterAuthInfos");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "EveMailSentTo");

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
                name: "MailLabels");

            migrationBuilder.DropTable(
                name: "MailLists");

            migrationBuilder.DropTable(
                name: "MailRecipients");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "PermissionRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "IndustryActivity",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "Mail",
                schema: "Eve");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ChatFiles");

            migrationBuilder.DropTable(
                name: "EveType",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "EveMailTemplate");

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
                name: "Account",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "EveAccount",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Corporations");

            migrationBuilder.DropTable(
                name: "Alliances");

            migrationBuilder.DropTable(
                name: "Icon",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "Faction",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "NpcCorporation",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "Race",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "SolarSystem",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "Constellation",
                schema: "Sde");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "Sde");
        }
    }
}
