using Microsoft.EntityFrameworkCore.Migrations;

namespace BookInspector.Data.Migrations
{
    public partial class _0404ChangesInForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookByAuthor_Author_BookId",
                table: "BookByAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingByBook_User_BookId",
                table: "RatingByBook");

            migrationBuilder.CreateIndex(
                name: "IX_RatingByBook_UserId",
                table: "RatingByBook",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookByAuthor_AuthorId",
                table: "BookByAuthor",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookByAuthor_Author_AuthorId",
                table: "BookByAuthor",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingByBook_User_UserId",
                table: "RatingByBook",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookByAuthor_Author_AuthorId",
                table: "BookByAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingByBook_User_UserId",
                table: "RatingByBook");

            migrationBuilder.DropIndex(
                name: "IX_RatingByBook_UserId",
                table: "RatingByBook");

            migrationBuilder.DropIndex(
                name: "IX_BookByAuthor_AuthorId",
                table: "BookByAuthor");

            migrationBuilder.AddForeignKey(
                name: "FK_BookByAuthor_Author_BookId",
                table: "BookByAuthor",
                column: "BookId",
                principalTable: "Author",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingByBook_User_BookId",
                table: "RatingByBook",
                column: "BookId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
