using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyBannerToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannerCategories_Banners_BannerId",
                table: "BannerCategories");

            migrationBuilder.DropIndex(
                name: "IX_BannerCategories_BannerId",
                table: "BannerCategories");

            migrationBuilder.DropColumn(
                name: "BannerId",
                table: "BannerCategories");

            migrationBuilder.AddColumn<int>(
                name: "BannerCategoryId",
                table: "Banners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Banners_BannerCategoryId",
                table: "Banners",
                column: "BannerCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_BannerCategories_BannerCategoryId",
                table: "Banners",
                column: "BannerCategoryId",
                principalTable: "BannerCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_BannerCategories_BannerCategoryId",
                table: "Banners");

            migrationBuilder.DropIndex(
                name: "IX_Banners_BannerCategoryId",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "BannerCategoryId",
                table: "Banners");

            migrationBuilder.AddColumn<int>(
                name: "BannerId",
                table: "BannerCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BannerCategories_BannerId",
                table: "BannerCategories",
                column: "BannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BannerCategories_Banners_BannerId",
                table: "BannerCategories",
                column: "BannerId",
                principalTable: "Banners",
                principalColumn: "Id");
        }
    }
}
