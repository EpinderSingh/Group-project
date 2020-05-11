using Microsoft.EntityFrameworkCore.Migrations;

namespace CoWork454.Migrations
{
    public partial class AddedLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "MailingList");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "MailingList",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "MailingList",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "MailingList");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "MailingList");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "MailingList",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
