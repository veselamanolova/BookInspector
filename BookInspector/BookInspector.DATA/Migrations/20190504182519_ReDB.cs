using Microsoft.EntityFrameworkCore.Migrations;

namespace BookInspector.DATA.Migrations
{
    public partial class ReDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Authors_AuthorId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Books_BookId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteBook_Books_BookId",
                table: "FavoriteBook");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteBook_AspNetUsers_DbUserId",
                table: "FavoriteBook");

            migrationBuilder.DropIndex(
                name: "IX_Books_CategoryId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteBook",
                table: "FavoriteBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthors",
                table: "BookAuthors");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "FavoriteBook",
                newName: "FavoriteBooks");

            migrationBuilder.RenameTable(
                name: "BookAuthors",
                newName: "BookAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteBook_BookId",
                table: "FavoriteBooks",
                newName: "IX_FavoriteBooks_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthor",
                newName: "IX_BookAuthor_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteBooks",
                table: "FavoriteBooks",
                columns: new[] { "DbUserId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                columns: new[] { "BookId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Authors_AuthorId",
                table: "BookAuthor",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Books_BookId",
                table: "BookAuthor",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteBooks_Books_BookId",
                table: "FavoriteBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteBooks_AspNetUsers_DbUserId",
                table: "FavoriteBooks",
                column: "DbUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Authors_AuthorId",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Books_BookId",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteBooks_Books_BookId",
                table: "FavoriteBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteBooks_AspNetUsers_DbUserId",
                table: "FavoriteBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteBooks",
                table: "FavoriteBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor");

            migrationBuilder.RenameTable(
                name: "FavoriteBooks",
                newName: "FavoriteBook");

            migrationBuilder.RenameTable(
                name: "BookAuthor",
                newName: "BookAuthors");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteBooks_BookId",
                table: "FavoriteBook",
                newName: "IX_FavoriteBook_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "BookAuthors",
                newName: "IX_BookAuthors_AuthorId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Books",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteBook",
                table: "FavoriteBook",
                columns: new[] { "DbUserId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthors",
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Authors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Books_BookId",
                table: "BookAuthors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteBook_Books_BookId",
                table: "FavoriteBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteBook_AspNetUsers_DbUserId",
                table: "FavoriteBook",
                column: "DbUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
