using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthLoginDemo_bnd.Migrations
{
    public partial class mg3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAddress",
                table: "ApplicationUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UserRole",
                table: "ApplicationUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "ApplicationUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAddress",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "ApplicationUsers");
        }
    }
}
