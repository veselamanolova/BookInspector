using Microsoft.EntityFrameworkCore.Migrations;

namespace BookInspector.DATA.Migrations
{
    public partial class addPreviewLinkInBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviewLink",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewLink",
                table: "Books");
        }
    }
}
