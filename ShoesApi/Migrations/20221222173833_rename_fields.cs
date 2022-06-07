using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class rename_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fname",
                table: "users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "addres",
                table: "orders",
                newName: "address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "users",
                newName: "fname");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "orders",
                newName: "addres");
        }
    }
}
