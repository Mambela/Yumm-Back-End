using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class addRestaurantTypeToRestaurantTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantTypes_RestaurantTypeId",
                table: "Restaurants");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantTypeId",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantTypes_RestaurantTypeId",
                table: "Restaurants",
                column: "RestaurantTypeId",
                principalTable: "RestaurantTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantTypes_RestaurantTypeId",
                table: "Restaurants");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantTypeId",
                table: "Restaurants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantTypes_RestaurantTypeId",
                table: "Restaurants",
                column: "RestaurantTypeId",
                principalTable: "RestaurantTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
