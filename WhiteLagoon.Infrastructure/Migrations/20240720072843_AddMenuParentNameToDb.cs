using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuParentNameToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MenuParentName",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuParentName",
                table: "Menus");
        }
    }
}
