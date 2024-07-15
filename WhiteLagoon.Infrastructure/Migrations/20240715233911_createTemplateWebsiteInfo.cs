using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createTemplateWebsiteInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WebsiteAdress",
                table: "WebsiteInfomations",
                newName: "WebsiteAddress");

            migrationBuilder.InsertData(
                table: "WebsiteInfomations",
                columns: new[] { "Id", "Fax", "WebsiteAddress", "WebsiteAdminTitle", "WebsiteEmail", "WebsiteEmail2", "WebsiteEmail3", "WebsiteName", "WebsitePhoneNumber", "WebsitePhoneNumber2", "WebsitePhoneNumber3", "WebsiteTitle", "WebsiteUrl" },
                values: new object[] { 1, null, "Your Address", null, null, null, null, "Your name website", "Your PhoneNumber", null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WebsiteInfomations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "WebsiteAddress",
                table: "WebsiteInfomations",
                newName: "WebsiteAdress");
        }
    }
}
