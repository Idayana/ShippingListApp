using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingListApi.Migrations
{
    public partial class RemoveIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductName",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);
        }
    }
}
