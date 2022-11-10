using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveMailHelper.DataAccessLayer.Migrations
{
    public partial class AuthInfoExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "CharacterAuthInfos",
                type: "nvarchar(768)",
                maxLength: 768,
                nullable: false,
                comment: "oauth refreshtoken",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldComment: "oauth refreshtoken");

            migrationBuilder.AlterColumn<string>(
                name: "AccessToken",
                table: "CharacterAuthInfos",
                type: "nvarchar(768)",
                maxLength: 768,
                nullable: false,
                comment: "oauth accesstoken",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldComment: "oauth accesstoken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "CharacterAuthInfos",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                comment: "oauth refreshtoken",
                oldClrType: typeof(string),
                oldType: "nvarchar(768)",
                oldMaxLength: 768,
                oldComment: "oauth refreshtoken");

            migrationBuilder.AlterColumn<string>(
                name: "AccessToken",
                table: "CharacterAuthInfos",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                comment: "oauth accesstoken",
                oldClrType: typeof(string),
                oldType: "nvarchar(768)",
                oldMaxLength: 768,
                oldComment: "oauth accesstoken");
        }
    }
}
