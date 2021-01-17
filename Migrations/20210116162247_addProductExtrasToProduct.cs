using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class addProductExtrasToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductsExtras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsExtras_ProductId",
                table: "ProductsExtras",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsExtras_Products_ProductId",
                table: "ProductsExtras",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsExtras_Products_ProductId",
                table: "ProductsExtras");

            migrationBuilder.DropIndex(
                name: "IX_ProductsExtras_ProductId",
                table: "ProductsExtras");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductsExtras");
        }
    }
}
