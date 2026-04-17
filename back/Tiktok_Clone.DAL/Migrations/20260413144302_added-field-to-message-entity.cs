using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tiktok_Clone.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedfieldtomessageentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "Messages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "Messages");
        }
    }
}
