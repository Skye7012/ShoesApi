using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brands", x => x.id);
                },
                comment: "Брэнды");

            migrationBuilder.CreateTable(
                name: "destinations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_destinations", x => x.id);
                },
                comment: "Назначение обуви");

            migrationBuilder.CreateTable(
                name: "seasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_seasons", x => x.id);
                },
                comment: "Сезон");

            migrationBuilder.CreateTable(
                name: "shoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    image = table.Column<string>(type: "text", nullable: false, comment: "Название изображения (для пути)"),
                    price = table.Column<int>(type: "integer", nullable: true, comment: "Цена"),
                    brand_id = table.Column<int>(type: "integer", nullable: true),
                    destination_id = table.Column<int>(type: "integer", nullable: true),
                    season_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shoes", x => x.id);
                    table.ForeignKey(
                        name: "fk_shoes_brands_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_shoes_destinations_destination_id",
                        column: x => x.destination_id,
                        principalTable: "destinations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_shoes_seasons_season_id",
                        column: x => x.season_id,
                        principalTable: "seasons",
                        principalColumn: "id");
                },
                comment: "Обувь");

            migrationBuilder.CreateIndex(
                name: "ix_brands_name",
                table: "brands",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_destinations_name",
                table: "destinations",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_seasons_name",
                table: "seasons",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_shoes_brand_id",
                table: "shoes",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "ix_shoes_destination_id",
                table: "shoes",
                column: "destination_id");

            migrationBuilder.CreateIndex(
                name: "ix_shoes_image",
                table: "shoes",
                column: "image",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_shoes_name",
                table: "shoes",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_shoes_season_id",
                table: "shoes",
                column: "season_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shoes");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "destinations");

            migrationBuilder.DropTable(
                name: "seasons");
        }
    }
}
