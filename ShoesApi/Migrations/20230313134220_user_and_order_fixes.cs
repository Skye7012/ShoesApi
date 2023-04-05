using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class user_and_order_fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_shoe");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "users",
                newName: "surname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surname",
                table: "users",
                newName: "first_name");

            migrationBuilder.CreateTable(
                name: "order_shoe",
                columns: table => new
                {
                    orders_id = table.Column<int>(type: "integer", nullable: false),
                    shoes_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_shoe", x => new { x.orders_id, x.shoes_id });
                    table.ForeignKey(
                        name: "fk_order_shoe_orders_orders_id",
                        column: x => x.orders_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_shoe_shoes_shoes_id",
                        column: x => x.shoes_id,
                        principalTable: "shoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_shoe_shoes_id",
                table: "order_shoe",
                column: "shoes_id");
        }
    }
}
