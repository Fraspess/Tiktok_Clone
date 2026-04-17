using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tiktok_Clone.DAL.Migrations
{
    /// <inheritdoc />
    public partial class userauthprovider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthProvider",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthProvider",
                table: "AspNetUsers");
        }
    }
}
