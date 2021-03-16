using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class addOrderProductsExtraToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TakenOberDate",
                table: "Orders",
                newName: "ReceivedDate");

            migrationBuilder.CreateTable(
                name: "OrderProductExtras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdersProductsId = table.Column<int>(type: "int", nullable: true),
                    ProductExtrasId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProductExtras_OrdersProducts_OrdersProductsId",
                        column: x => x.OrdersProductsId,
                        principalTable: "OrdersProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderProductExtras_ProductsExtras_ProductExtrasId",
                        column: x => x.ProductExtrasId,
                        principalTable: "ProductsExtras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductExtras_OrdersProductsId",
                table: "OrderProductExtras",
                column: "OrdersProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductExtras_ProductExtrasId",
                table: "OrderProductExtras",
                column: "ProductExtrasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProductExtras");

            migrationBuilder.RenameColumn(
                name: "ReceivedDate",
                table: "Orders",
                newName: "TakenOberDate");
        }
    }
}
