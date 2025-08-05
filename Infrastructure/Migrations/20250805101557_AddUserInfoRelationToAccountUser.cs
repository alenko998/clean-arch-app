using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserInfoRelationToAccountUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
