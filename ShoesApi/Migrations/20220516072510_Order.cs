using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "sizes",
                comment: "Размеры");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    oder_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sum = table.Column<int>(type: "integer", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                },
                comment: "Заказ");

            migrationBuilder.CreateTable(
                name: "order_shoe",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    shoe_id = table.Column<int>(type: "integer", nullable: false),
                    size_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_shoe", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_shoe_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_shoe_shoes_shoe_id",
                        column: x => x.shoe_id,
                        principalTable: "shoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_shoe_sizes_size_id",
                        column: x => x.size_id,
                        principalTable: "sizes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_sizes_ru_size",
                table: "sizes",
                column: "ru_size",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_order_shoe_order_id",
                table: "order_shoe",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_shoe_shoe_id",
                table: "order_shoe",
                column: "shoe_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_shoe_size_id",
                table: "order_shoe",
                column: "size_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_shoe");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropIndex(
                name: "ix_sizes_ru_size",
                table: "sizes");

            migrationBuilder.AlterTable(
                name: "sizes",
                oldComment: "Размеры");
        }
    }
}
