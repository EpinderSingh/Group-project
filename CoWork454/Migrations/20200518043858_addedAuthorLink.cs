using Microsoft.EntityFrameworkCore.Migrations;

namespace CoWork454.Migrations
{
    public partial class addedAuthorLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId1",
                table: "NewsPosts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsPosts_AuthorId",
                table: "NewsPosts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsPosts_AuthorId1",
                table: "NewsPosts",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsPosts_Users_AuthorId",
                table: "NewsPosts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsPosts_Users_AuthorId1",
                table: "NewsPosts",
                column: "AuthorId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsPosts_Users_AuthorId",
                table: "NewsPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsPosts_Users_AuthorId1",
                table: "NewsPosts");

            migrationBuilder.DropIndex(
                name: "IX_NewsPosts_AuthorId",
                table: "NewsPosts");

            migrationBuilder.DropIndex(
                name: "IX_NewsPosts_AuthorId1",
                table: "NewsPosts");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "NewsPosts");
        }
    }
}
