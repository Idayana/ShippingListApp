using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingListApi.Migrations
{
    public partial class ModifiedProductEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_UnitMeasurements_UnitMeasurementId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UnitMeasurementId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitMeasurementId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<float>(
                name: "Quantity",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "UnitMeasurementId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitMeasurementId",
                table: "Products",
                column: "UnitMeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_UnitMeasurements_UnitMeasurementId",
                table: "Products",
                column: "UnitMeasurementId",
                principalTable: "UnitMeasurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
