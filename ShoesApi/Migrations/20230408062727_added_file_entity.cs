using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class added_file_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_shoes_image",
                table: "shoes");

            migrationBuilder.DropColumn(
                name: "image",
                table: "shoes");

            migrationBuilder.AddColumn<int>(
                name: "image_file_id",
                table: "shoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_files", x => x.id);
                },
                comment: "Файлы");

            migrationBuilder.CreateIndex(
                name: "ix_shoes_image_file_id",
                table: "shoes",
                column: "image_file_id");

            migrationBuilder.AddForeignKey(
                name: "fk_shoes_files_image_file_id",
                table: "shoes",
                column: "image_file_id",
                principalTable: "files",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_shoes_files_image_file_id",
                table: "shoes");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropIndex(
                name: "ix_shoes_image_file_id",
                table: "shoes");

            migrationBuilder.DropColumn(
                name: "image_file_id",
                table: "shoes");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "shoes",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Название изображения (для пути)");

            migrationBuilder.CreateIndex(
                name: "ix_shoes_image",
                table: "shoes",
                column: "image",
                unique: true);
        }
    }
}
