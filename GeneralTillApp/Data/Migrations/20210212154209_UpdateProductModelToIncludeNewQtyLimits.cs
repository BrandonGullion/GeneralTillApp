using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneralTillApp.Data.Migrations
{
    public partial class UpdateProductModelToIncludeNewQtyLimits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtyDiscounted",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtyPurchaseLimit",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtyDiscounted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QtyPurchaseLimit",
                table: "Products");
        }
    }
}
