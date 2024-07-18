using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addMenuAndMenuCatToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Menus",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   MenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   MenuLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Order = table.Column<int>(type: "int", nullable: false),
                   IsActive = table.Column<bool>(type: "bit", nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Menus", x => x.Id);
               });

            migrationBuilder.CreateTable(
                name: "MenuCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCategories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "MenuCategories");


            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
