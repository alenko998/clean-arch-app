using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteUserInfoOnAccountUserDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountUsers_UserInfos_UserInfoId",
                table: "AccountUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUsers_UserInfos_UserInfoId",
                table: "AccountUsers",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountUsers_UserInfos_UserInfoId",
                table: "AccountUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUsers_UserInfos_UserInfoId",
                table: "AccountUsers",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
