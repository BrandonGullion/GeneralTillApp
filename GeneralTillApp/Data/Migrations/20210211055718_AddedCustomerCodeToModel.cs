using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneralTillApp.Data.Migrations
{
    public partial class AddedCustomerCodeToModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerCode",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ItemSubtotal",
                table: "CartItem",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ItemSubtotal",
                table: "CartItem");
        }
    }
}
