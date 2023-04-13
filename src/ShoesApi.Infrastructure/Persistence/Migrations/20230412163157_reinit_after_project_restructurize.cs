using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShoesApi.Infrastructure.Persistence.Migrations
{
	public partial class reinit_after_project_restructurize : Migration
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
				name: "sizes",
				columns: table => new
				{
					id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					ru_size = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_sizes", x => x.id);
				},
				comment: "Размеры");

			migrationBuilder.CreateTable(
				name: "users",
				columns: table => new
				{
					id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					login = table.Column<string>(type: "text", nullable: false),
					password_hash = table.Column<byte[]>(type: "bytea", nullable: false),
					password_salt = table.Column<byte[]>(type: "bytea", nullable: false),
					name = table.Column<string>(type: "text", nullable: false),
					surname = table.Column<string>(type: "text", nullable: true),
					phone = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_users", x => x.id);
				},
				comment: "Пользователи");

			migrationBuilder.CreateTable(
				name: "shoes",
				columns: table => new
				{
					id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					name = table.Column<string>(type: "text", nullable: false),
					image_file_id = table.Column<int>(type: "integer", nullable: false),
					price = table.Column<int>(type: "integer", nullable: false, comment: "Цена"),
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
						name: "fk_shoes_files_image_file_id",
						column: x => x.image_file_id,
						principalTable: "files",
						principalColumn: "id");
					table.ForeignKey(
						name: "fk_shoes_seasons_season_id",
						column: x => x.season_id,
						principalTable: "seasons",
						principalColumn: "id");
				},
				comment: "Обувь");

			migrationBuilder.CreateTable(
				name: "orders",
				columns: table => new
				{
					id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					address = table.Column<string>(type: "text", nullable: false),
					sum = table.Column<int>(type: "integer", nullable: false),
					count = table.Column<int>(type: "integer", nullable: false),
					user_id = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_orders", x => x.id);
					table.ForeignKey(
						name: "fk_orders_users_user_id",
						column: x => x.user_id,
						principalTable: "users",
						principalColumn: "id");
				},
				comment: "Заказ");

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
						name: "fk_shoes_sizes_sizes_sizes_id",
						column: x => x.sizes_id,
						principalTable: "sizes",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "order_items",
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
					table.PrimaryKey("pk_order_items", x => x.id);
					table.ForeignKey(
						name: "fk_order_items_orders_order_id",
						column: x => x.order_id,
						principalTable: "orders",
						principalColumn: "id");
					table.ForeignKey(
						name: "fk_order_items_shoes_shoe_id",
						column: x => x.shoe_id,
						principalTable: "shoes",
						principalColumn: "id");
					table.ForeignKey(
						name: "fk_order_items_sizes_size_id",
						column: x => x.size_id,
						principalTable: "sizes",
						principalColumn: "id");
				},
				comment: "Часть заказа");

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
				name: "ix_order_items_order_id_shoe_id_size_id",
				table: "order_items",
				columns: new[] { "order_id", "shoe_id", "size_id" },
				unique: true);

			migrationBuilder.CreateIndex(
				name: "ix_order_items_shoe_id",
				table: "order_items",
				column: "shoe_id");

			migrationBuilder.CreateIndex(
				name: "ix_order_items_size_id",
				table: "order_items",
				column: "size_id");

			migrationBuilder.CreateIndex(
				name: "ix_orders_user_id",
				table: "orders",
				column: "user_id");

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
				name: "ix_shoes_image_file_id",
				table: "shoes",
				column: "image_file_id");

			migrationBuilder.CreateIndex(
				name: "ix_shoes_name",
				table: "shoes",
				column: "name",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "ix_shoes_season_id",
				table: "shoes",
				column: "season_id");

			migrationBuilder.CreateIndex(
				name: "ix_shoes_sizes_sizes_id",
				table: "shoes_sizes",
				column: "sizes_id");

			migrationBuilder.CreateIndex(
				name: "ix_sizes_ru_size",
				table: "sizes",
				column: "ru_size",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "ix_users_login",
				table: "users",
				column: "login",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "order_items");

			migrationBuilder.DropTable(
				name: "shoes_sizes");

			migrationBuilder.DropTable(
				name: "orders");

			migrationBuilder.DropTable(
				name: "shoes");

			migrationBuilder.DropTable(
				name: "sizes");

			migrationBuilder.DropTable(
				name: "users");

			migrationBuilder.DropTable(
				name: "brands");

			migrationBuilder.DropTable(
				name: "destinations");

			migrationBuilder.DropTable(
				name: "files");

			migrationBuilder.DropTable(
				name: "seasons");
		}
	}
}
