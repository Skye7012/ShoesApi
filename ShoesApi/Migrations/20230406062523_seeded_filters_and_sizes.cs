using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class seeded_filters_and_sizes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Nike" },
                    { 2, "Adidas" },
                    { 3, "Reebok" },
                    { 4, "Asics" }
                });

            migrationBuilder.InsertData(
                table: "destinations",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Повседневность" },
                    { 2, "Баскетбол" },
                    { 3, "Волейбол" },
                    { 4, "Бег" }
                });

            migrationBuilder.InsertData(
                table: "seasons",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Лето" },
                    { 2, "Демисезон" },
                    { 3, "Зима" }
                });

            migrationBuilder.InsertData(
                table: "sizes",
                columns: new[] { "id", "ru_size" },
                values: new object[,]
                {
                    { 1, 38 },
                    { 2, 39 },
                    { 3, 40 },
                    { 4, 41 },
                    { 5, 42 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "destinations",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "destinations",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "destinations",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "destinations",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "seasons",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "seasons",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "seasons",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "sizes",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "sizes",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "sizes",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "sizes",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "sizes",
                keyColumn: "id",
                keyValue: 5);
        }
    }
}
