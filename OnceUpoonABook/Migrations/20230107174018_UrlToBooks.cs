using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnceUpoonABook.Migrations
{
    /// <inheritdoc />
    public partial class UrlToBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverURL",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverURL",
                table: "Books");
        }
    }
}
