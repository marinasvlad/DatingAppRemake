using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class LikeEntityAddedChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableLike_Users_SourceUserId",
                table: "TableLike");

            migrationBuilder.DropForeignKey(
                name: "FK_TableLike_Users_TargetUserId",
                table: "TableLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableLike",
                table: "TableLike");

            migrationBuilder.RenameTable(
                name: "TableLike",
                newName: "Likes");

            migrationBuilder.RenameIndex(
                name: "IX_TableLike_TargetUserId",
                table: "Likes",
                newName: "IX_Likes_TargetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "SourceUserId", "TargetUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_SourceUserId",
                table: "Likes",
                column: "SourceUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_TargetUserId",
                table: "Likes",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_SourceUserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_TargetUserId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "TableLike");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_TargetUserId",
                table: "TableLike",
                newName: "IX_TableLike_TargetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableLike",
                table: "TableLike",
                columns: new[] { "SourceUserId", "TargetUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TableLike_Users_SourceUserId",
                table: "TableLike",
                column: "SourceUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableLike_Users_TargetUserId",
                table: "TableLike",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
