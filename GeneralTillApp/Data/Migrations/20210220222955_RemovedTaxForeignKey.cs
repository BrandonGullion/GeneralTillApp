using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneralTillApp.Data.Migrations
{
    public partial class RemovedTaxForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Tax_TaxId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TaxId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaxId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartGST = table.Column<double>(type: "float", nullable: false),
                    CartPST = table.Column<double>(type: "float", nullable: false),
                    GST = table.Column<double>(type: "float", nullable: false),
                    PST = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TaxId",
                table: "Transactions",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Tax_TaxId",
                table: "Transactions",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
