using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneralTillApp.Data.Migrations
{
    public partial class MoveAllClassesOver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ProductsInGroup = table.Column<int>(nullable: false),
                    Promotion = table.Column<bool>(nullable: false),
                    PromotionPercent = table.Column<double>(nullable: false),
                    PromotionAmount = table.Column<double>(nullable: false),
                    PromoStartDate = table.Column<DateTime>(nullable: false),
                    PromoEndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    UPC = table.Column<string>(nullable: false),
                    Discountable = table.Column<bool>(nullable: false),
                    PST = table.Column<bool>(nullable: false),
                    GST = table.Column<bool>(nullable: false),
                    OnHand = table.Column<int>(nullable: false),
                    UnitSize = table.Column<int>(nullable: false),
                    UnitOfMeasureId = table.Column<int>(nullable: false),
                    UnitOfMeasure = table.Column<string>(nullable: true),
                    ShelfMin = table.Column<int>(nullable: false),
                    ShelfMax = table.Column<int>(nullable: false),
                    ProductGroupId = table.Column<int>(nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    AcqCost = table.Column<double>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    LastEdited = table.Column<DateTime>(nullable: false),
                    OnOrder = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductGroupId",
                table: "Products",
                column: "ProductGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "UnitOfMeasures");

            migrationBuilder.DropTable(
                name: "ProductGroups");
        }
    }
}
