using Microsoft.EntityFrameworkCore.Migrations;

namespace assetManagementBackend.Migrations
{
    public partial class migration_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Users_user_id",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_user_id",
                table: "Assets");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "Assets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "Assets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
