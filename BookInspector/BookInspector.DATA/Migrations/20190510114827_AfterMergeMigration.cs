using Microsoft.EntityFrameworkCore.Migrations;

namespace BookInspector.DATA.Migrations
{
    public partial class AfterMergeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteBooks_AspNetUsers_ApplicationUserId",
                table: "FavoriteBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBookRating_AspNetUsers_ApplicationUserId",
                table: "UserBookRating");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "UserBookRating",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBookRating_ApplicationUserId",
                table: "UserBookRating",
                newName: "IX_UserBookRating_UserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "FavoriteBooks",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteBooks_AspNetUsers_UserId",
                table: "FavoriteBooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookRating_AspNetUsers_UserId",
                table: "UserBookRating",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteBooks_AspNetUsers_UserId",
                table: "FavoriteBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBookRating_AspNetUsers_UserId",
                table: "UserBookRating");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserBookRating",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBookRating_UserId",
                table: "UserBookRating",
                newName: "IX_UserBookRating_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavoriteBooks",
                newName: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteBooks_AspNetUsers_ApplicationUserId",
                table: "FavoriteBooks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookRating_AspNetUsers_ApplicationUserId",
                table: "UserBookRating",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
