using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoWork454.Migrations
{
    public partial class addedNewsPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(nullable: false),
                    DateTimePosted = table.Column<DateTimeOffset>(nullable: false),
                    NewsTitle = table.Column<string>(nullable: true),
                    NewsText = table.Column<string>(nullable: true),
                    NewsPhoto = table.Column<string>(nullable: true),
                    NewsTag = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPosts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsPosts");
        }
    }
}
