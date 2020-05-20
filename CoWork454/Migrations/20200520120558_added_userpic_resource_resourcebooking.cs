using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoWork454.Migrations
{
    public partial class added_userpic_resource_resourcebooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserImagePath",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(nullable: true),
                    ResourceDescription = table.Column<string>(nullable: true),
                    ResourceImage = table.Column<string>(nullable: true),
                    ResourceMaxCapacity = table.Column<int>(nullable: false),
                    ResourceHasVC = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceBookings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ResourceId = table.Column<int>(nullable: false),
                    ResourceBookingTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    ResourceBookingStart = table.Column<DateTimeOffset>(nullable: false),
                    ResourceBookingEnd = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceBookings_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceBookings_ResourceId",
                table: "ResourceBookings",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceBookings_UserId",
                table: "ResourceBookings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceBookings");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropColumn(
                name: "UserImagePath",
                table: "Users");
        }
    }
}
