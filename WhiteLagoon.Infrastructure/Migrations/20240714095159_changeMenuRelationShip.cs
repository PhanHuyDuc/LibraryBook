using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeMenuRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubSubMenus");

            migrationBuilder.DropTable(
                name: "SubMenus");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MainMenus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "MainMenus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "MainMenus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MainMenus");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "MainMenus");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "MainMenus");

            migrationBuilder.CreateTable(
                name: "SubMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainMenuId = table.Column<int>(type: "int", nullable: false),
                    SubMenuLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubMenuName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMenus_MainMenus_MainMenuId",
                        column: x => x.MainMenuId,
                        principalTable: "MainMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubSubMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubMenuId = table.Column<int>(type: "int", nullable: false),
                    SubSubMenuLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubSubMenuName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSubMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSubMenus_SubMenus_SubMenuId",
                        column: x => x.SubMenuId,
                        principalTable: "SubMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubMenus_MainMenuId",
                table: "SubMenus",
                column: "MainMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SubSubMenus_SubMenuId",
                table: "SubSubMenus",
                column: "SubMenuId");
        }
    }
}
