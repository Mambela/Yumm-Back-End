using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class AddIdOrderProductsIdandProductExtrasId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductExtras_OrdersProducts_OrdersProductsId",
                table: "OrderProductExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductExtras_ProductsExtras_ProductExtrasId",
                table: "OrderProductExtras");

            migrationBuilder.AlterColumn<int>(
                name: "ProductExtrasId",
                table: "OrderProductExtras",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrdersProductsId",
                table: "OrderProductExtras",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductExtras_OrdersProducts_OrdersProductsId",
                table: "OrderProductExtras",
                column: "OrdersProductsId",
                principalTable: "OrdersProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductExtras_ProductsExtras_ProductExtrasId",
                table: "OrderProductExtras",
                column: "ProductExtrasId",
                principalTable: "ProductsExtras",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductExtras_OrdersProducts_OrdersProductsId",
                table: "OrderProductExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductExtras_ProductsExtras_ProductExtrasId",
                table: "OrderProductExtras");

            migrationBuilder.AlterColumn<int>(
                name: "ProductExtrasId",
                table: "OrderProductExtras",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrdersProductsId",
                table: "OrderProductExtras",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductExtras_OrdersProducts_OrdersProductsId",
                table: "OrderProductExtras",
                column: "OrdersProductsId",
                principalTable: "OrdersProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductExtras_ProductsExtras_ProductExtrasId",
                table: "OrderProductExtras",
                column: "ProductExtrasId",
                principalTable: "ProductsExtras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
