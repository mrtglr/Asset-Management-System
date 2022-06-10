using Microsoft.EntityFrameworkCore.Migrations;

namespace assetManagementBackend.Migrations
{
    public partial class migration_v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "LogOperations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "LogOperations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
