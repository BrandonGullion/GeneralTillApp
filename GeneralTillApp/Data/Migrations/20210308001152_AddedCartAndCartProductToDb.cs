using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneralTillApp.Data.Migrations
{
    public partial class AddedCartAndCartProductToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Transactions_TransactionId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CartItems_CartId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_TransactionId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "QtyDiscounted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QtyPurchaseLimit",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "Carts");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_ProductId",
                table: "Carts",
                newName: "IX_Carts_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercent",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Discounted",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountedPrice",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QtyInCart",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Products",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CartId",
                table: "Transactions",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Carts_CartId",
                table: "Products",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Carts_CartId",
                table: "Transactions",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Carts_CartId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Carts_CartId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CartId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Discounted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QtyInCart",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "CartItems");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ProductId",
                table: "CartItems",
                newName: "IX_CartItems_ProductId");

            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercent",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "QtyDiscounted",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtyPurchaseLimit",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_TransactionId",
                table: "CartItems",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Transactions_TransactionId",
                table: "CartItems",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CartItems_CartId",
                table: "Products",
                column: "CartId",
                principalTable: "CartItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
