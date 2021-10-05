using Microsoft.EntityFrameworkCore.Migrations;

namespace assetManagementBackend.Migrations
{
    public partial class migration_v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "block",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "parcel",
                table: "Assets",
                newName: "worth");

            migrationBuilder.AddColumn<int>(
                name: "totalAssetWorth",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalAssetWorth",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "worth",
                table: "Assets",
                newName: "parcel");

            migrationBuilder.AddColumn<int>(
                name: "block",
                table: "Assets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
