using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreColumnContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "Description2",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description3",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description2",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Description3",
                table: "Contents");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Contents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
