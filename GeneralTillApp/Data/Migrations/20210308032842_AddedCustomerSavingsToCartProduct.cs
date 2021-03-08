using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneralTillApp.Data.Migrations
{
    public partial class AddedCustomerSavingsToCartProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Carts");

            migrationBuilder.AddColumn<double>(
                name: "CustomerSavings",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartProductId",
                table: "Carts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CartProductId",
                table: "Carts",
                column: "CartProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_CartProductId",
                table: "Carts",
                column: "CartProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_CartProductId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CartProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CustomerSavings",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartProductId",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
