using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tiktok_Clone.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addUniqueConstraintToLikeCommentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CommentLikeEntity_UserId",
                table: "CommentLikeEntity");

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikeEntity_UserId_CommentId",
                table: "CommentLikeEntity",
                columns: new[] { "UserId", "CommentId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CommentLikeEntity_UserId_CommentId",
                table: "CommentLikeEntity");

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikeEntity_UserId",
                table: "CommentLikeEntity",
                column: "UserId");
        }
    }
}
