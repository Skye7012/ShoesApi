using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class userUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fname",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "shoes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Цена",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComment: "Цена");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fname",
                table: "users");

            migrationBuilder.DropColumn(
                name: "name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "shoes",
                type: "integer",
                nullable: true,
                comment: "Цена",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Цена");
        }
    }
}
