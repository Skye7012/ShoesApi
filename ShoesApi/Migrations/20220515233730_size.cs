using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class size : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "size",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ru_size = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_size", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shoes_sizes",
                columns: table => new
                {
                    shoes_id = table.Column<int>(type: "integer", nullable: false),
                    sizes_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shoes_sizes", x => new { x.shoes_id, x.sizes_id });
                    table.ForeignKey(
                        name: "fk_shoes_sizes_shoes_shoes_id",
                        column: x => x.shoes_id,
                        principalTable: "shoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_shoes_sizes_size_sizes_id",
                        column: x => x.sizes_id,
                        principalTable: "size",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_shoes_sizes_sizes_id",
                table: "shoes_sizes",
                column: "sizes_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shoes_sizes");

            migrationBuilder.DropTable(
                name: "size");
        }
    }
}
