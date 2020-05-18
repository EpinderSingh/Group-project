using Microsoft.EntityFrameworkCore.Migrations;

namespace CoWork454.Migrations
{
    public partial class addedNewsPostLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsPosts_Users_AuthorId1",
                table: "NewsPosts");

            migrationBuilder.DropIndex(
                name: "IX_NewsPosts_AuthorId1",
                table: "NewsPosts");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "NewsPosts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId1",
                table: "NewsPosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsPosts_AuthorId1",
                table: "NewsPosts",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsPosts_Users_AuthorId1",
                table: "NewsPosts",
                column: "AuthorId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
