using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserInfoToWriter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountUsers_UserInfos_UserInfoId",
                table: "AccountUsers");

            migrationBuilder.DropIndex(
                name: "IX_AccountUsers_UserInfoId",
                table: "AccountUsers");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "AccountUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Writers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Writers_UserInfoId",
                table: "Writers",
                column: "UserInfoId",
                unique: true,
                filter: "[UserInfoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Writers_UserInfos_UserInfoId",
                table: "Writers",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Writers_UserInfos_UserInfoId",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Writers_UserInfoId",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Writers");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "AccountUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountUsers_UserInfoId",
                table: "AccountUsers",
                column: "UserInfoId",
                unique: true,
                filter: "[UserInfoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUsers_UserInfos_UserInfoId",
                table: "AccountUsers",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
