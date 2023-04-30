using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class modifyOrderTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderedByUserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessedByUserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderedByUserId",
                table: "Orders",
                column: "OrderedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProcessedByUserId",
                table: "Orders",
                column: "ProcessedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_OrderedByUserId",
                table: "Orders",
                column: "OrderedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ProcessedByUserId",
                table: "Orders",
                column: "ProcessedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_OrderedByUserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ProcessedByUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderedByUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProcessedByUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderedByUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProcessedByUserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "Price");
        }
    }
}
