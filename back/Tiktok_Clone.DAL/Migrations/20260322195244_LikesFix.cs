using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tiktok_Clone.DAL.Migrations
{
    /// <inheritdoc />
    public partial class LikesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeEntity_AspNetUsers_UserId",
                table: "LikeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeEntity_Videos_VideoId",
                table: "LikeEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LikeEntity",
                table: "LikeEntity");

            migrationBuilder.RenameTable(
                name: "LikeEntity",
                newName: "Likes");

            migrationBuilder.RenameIndex(
                name: "IX_LikeEntity_VideoId",
                table: "Likes",
                newName: "IX_Likes_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_LikeEntity_UserId_VideoId",
                table: "Likes",
                newName: "IX_Likes_UserId_VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Videos_VideoId",
                table: "Likes",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Videos_VideoId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "LikeEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_VideoId",
                table: "LikeEntity",
                newName: "IX_LikeEntity_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId_VideoId",
                table: "LikeEntity",
                newName: "IX_LikeEntity_UserId_VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikeEntity",
                table: "LikeEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeEntity_AspNetUsers_UserId",
                table: "LikeEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeEntity_Videos_VideoId",
                table: "LikeEntity",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
