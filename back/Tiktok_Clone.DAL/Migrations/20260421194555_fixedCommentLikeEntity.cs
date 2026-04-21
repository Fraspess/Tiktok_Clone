using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tiktok_Clone.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixedCommentLikeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentLikeEntity",
                table: "CommentLikeEntity");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CommentLikeEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentLikeEntity",
                table: "CommentLikeEntity",
                columns: new[] { "UserId", "CommentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentLikeEntity",
                table: "CommentLikeEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CommentLikeEntity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentLikeEntity",
                table: "CommentLikeEntity",
                column: "Id");
        }
    }
}
