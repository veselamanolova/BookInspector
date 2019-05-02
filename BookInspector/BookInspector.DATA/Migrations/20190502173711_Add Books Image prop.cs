using Microsoft.EntityFrameworkCore.Migrations;

namespace BookInspector.DATA.Migrations
{
    public partial class AddBooksImageprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Books");
        }
    }
}
