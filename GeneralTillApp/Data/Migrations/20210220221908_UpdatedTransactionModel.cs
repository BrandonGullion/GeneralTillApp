using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneralTillApp.Data.Migrations
{
    public partial class UpdatedTransactionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseTime",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Products");

            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "Transactions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercent",
                table: "Transactions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountSubtotal",
                table: "Transactions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Transactions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TransactionNumber",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CartGST",
                table: "Tax",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CartPST",
                table: "Tax",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AdjustDiscountSavings",
                table: "CartItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "AmountDiscountedBool",
                table: "CartItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "CustomerDiscountSavings",
                table: "CartItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CustomerDiscountTotal",
                table: "CartItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "CartItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercent",
                table: "CartItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountSubTotal",
                table: "CartItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Discounted",
                table: "CartItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PercentDiscountedBool",
                table: "CartItem",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DiscountSubtotal",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionNumber",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CartGST",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CartPST",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "AdjustDiscountSavings",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "AmountDiscountedBool",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "CustomerDiscountSavings",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "CustomerDiscountTotal",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "DiscountSubTotal",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "Discounted",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "PercentDiscountedBool",
                table: "CartItem");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseTime",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercent",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
