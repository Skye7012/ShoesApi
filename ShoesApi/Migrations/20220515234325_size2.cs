using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class size2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_shoes_sizes_size_sizes_id",
                table: "shoes_sizes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_size",
                table: "size");

            migrationBuilder.RenameTable(
                name: "size",
                newName: "sizes");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sizes",
                table: "sizes",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_shoes_sizes_sizes_sizes_id",
                table: "shoes_sizes",
                column: "sizes_id",
                principalTable: "sizes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_shoes_sizes_sizes_sizes_id",
                table: "shoes_sizes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sizes",
                table: "sizes");

            migrationBuilder.RenameTable(
                name: "sizes",
                newName: "size");

            migrationBuilder.AddPrimaryKey(
                name: "pk_size",
                table: "size",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_shoes_sizes_size_sizes_id",
                table: "shoes_sizes",
                column: "sizes_id",
                principalTable: "size",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
