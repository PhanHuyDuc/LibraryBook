using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyColumnToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaCategories_Medias_MediaId",
                table: "MediaCategories");

            migrationBuilder.DropIndex(
                name: "IX_MediaCategories_MediaId",
                table: "MediaCategories");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "MediaCategories");

            migrationBuilder.AddColumn<int>(
                name: "WidgetContentCategoryId",
                table: "WidgetContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MediaCategoryId",
                table: "Medias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentCategoryId",
                table: "Contents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WidgetContents_WidgetContentCategoryId",
                table: "WidgetContents",
                column: "WidgetContentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_MediaCategoryId",
                table: "Medias",
                column: "MediaCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ContentCategoryId",
                table: "Contents",
                column: "ContentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_ContentCategories_ContentCategoryId",
                table: "Contents",
                column: "ContentCategoryId",
                principalTable: "ContentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_MediaCategories_MediaCategoryId",
                table: "Medias",
                column: "MediaCategoryId",
                principalTable: "MediaCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WidgetContents_WidgetContentCategories_WidgetContentCategoryId",
                table: "WidgetContents",
                column: "WidgetContentCategoryId",
                principalTable: "WidgetContentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ContentCategories_ContentCategoryId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_MediaCategories_MediaCategoryId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_WidgetContents_WidgetContentCategories_WidgetContentCategoryId",
                table: "WidgetContents");

            migrationBuilder.DropIndex(
                name: "IX_WidgetContents_WidgetContentCategoryId",
                table: "WidgetContents");

            migrationBuilder.DropIndex(
                name: "IX_Medias_MediaCategoryId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ContentCategoryId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "WidgetContentCategoryId",
                table: "WidgetContents");

            migrationBuilder.DropColumn(
                name: "MediaCategoryId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "ContentCategoryId",
                table: "Contents");

            migrationBuilder.AddColumn<int>(
                name: "MediaId",
                table: "MediaCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaCategories_MediaId",
                table: "MediaCategories",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaCategories_Medias_MediaId",
                table: "MediaCategories",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "Id");
        }
    }
}
