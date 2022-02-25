using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AuthLoginDemo_bnd.Migrations
{
    public partial class mg4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogOperations",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    log_situation = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    process = table.Column<string>(type: "text", nullable: true),
                    date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    user_ip = table.Column<string>(type: "text", nullable: true),
                    statement = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogOperations", x => x.log_id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    province_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    province_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.province_id);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    district_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    district_name = table.Column<string>(type: "text", nullable: true),
                    province_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.district_id);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_province_id",
                        column: x => x.province_id,
                        principalTable: "Provinces",
                        principalColumn: "province_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Neighbourhoods",
                columns: table => new
                {
                    neighbourhood_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    district_id = table.Column<int>(type: "integer", nullable: false),
                    neighbourhood_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighbourhoods", x => x.neighbourhood_id);
                    table.ForeignKey(
                        name: "FK_Neighbourhoods_Districts_district_id",
                        column: x => x.district_id,
                        principalTable: "Districts",
                        principalColumn: "district_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    asset_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<string>(type: "text", nullable: true),
                    neighbourhood_id = table.Column<int>(type: "integer", nullable: false),
                    worth = table.Column<int>(type: "integer", nullable: false),
                    attribute = table.Column<string>(type: "text", nullable: true),
                    adress = table.Column<string>(type: "text", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.asset_id);
                    table.ForeignKey(
                        name: "FK_Assets_ApplicationUsers_Id",
                        column: x => x.Id,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Neighbourhoods_neighbourhood_id",
                        column: x => x.neighbourhood_id,
                        principalTable: "Neighbourhoods",
                        principalColumn: "neighbourhood_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_Id",
                table: "Assets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_neighbourhood_id",
                table: "Assets",
                column: "neighbourhood_id");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_province_id",
                table: "Districts",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_Neighbourhoods_district_id",
                table: "Neighbourhoods",
                column: "district_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "LogOperations");

            migrationBuilder.DropTable(
                name: "Neighbourhoods");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
