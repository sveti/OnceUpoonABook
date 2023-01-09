using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnceUpoonABook.Migrations
{
    /// <inheritdoc />
    public partial class EnumsToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookCategory",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookCategory",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Books");
        }
    }
}
