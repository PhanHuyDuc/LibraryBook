using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeMenuNameDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategories_MainMenus_MainMenuId",
                table: "MenuCategories");

            migrationBuilder.DropTable(
                name: "MainMenus");

            migrationBuilder.RenameColumn(
                name: "MainMenuId",
                table: "MenuCategories",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuCategories_MainMenuId",
                table: "MenuCategories",
                newName: "IX_MenuCategories_MenuId");

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategories_Menus_MenuId",
                table: "MenuCategories",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategories_Menus_MenuId",
                table: "MenuCategories");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "MenuCategories",
                newName: "MainMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuCategories_MenuId",
                table: "MenuCategories",
                newName: "IX_MenuCategories_MainMenuId");

            migrationBuilder.CreateTable(
                name: "MainMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MenuLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainMenus", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategories_MainMenus_MainMenuId",
                table: "MenuCategories",
                column: "MainMenuId",
                principalTable: "MainMenus",
                principalColumn: "Id");
        }
    }
}
