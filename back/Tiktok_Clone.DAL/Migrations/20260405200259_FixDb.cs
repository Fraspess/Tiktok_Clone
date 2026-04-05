using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tiktok_Clone.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteEntity_AspNetUsers_UserId",
                table: "FavoriteEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteEntity_Videos_VideoId",
                table: "FavoriteEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteEntity",
                table: "FavoriteEntity");

            migrationBuilder.RenameTable(
                name: "FavoriteEntity",
                newName: "Favorites");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteEntity_VideoId",
                table: "Favorites",
                newName: "IX_Favorites_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteEntity_UserId_VideoId",
                table: "Favorites",
                newName: "IX_Favorites_UserId_VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Videos_VideoId",
                table: "Favorites",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Videos_VideoId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.RenameTable(
                name: "Favorites",
                newName: "FavoriteEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_VideoId",
                table: "FavoriteEntity",
                newName: "IX_FavoriteEntity_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_UserId_VideoId",
                table: "FavoriteEntity",
                newName: "IX_FavoriteEntity_UserId_VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteEntity",
                table: "FavoriteEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteEntity_AspNetUsers_UserId",
                table: "FavoriteEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteEntity_Videos_VideoId",
                table: "FavoriteEntity",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
