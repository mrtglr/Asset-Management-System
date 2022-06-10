using Microsoft.EntityFrameworkCore.Migrations;

namespace assetManagementBackend.Migrations
{
    public partial class migration_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Users_user_id",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_user_id",
                table: "Assets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Assets_user_id",
                table: "Assets",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Users_user_id",
                table: "Assets",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
