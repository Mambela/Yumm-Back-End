using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class addRestaurantTypeToRestaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantTypeId",
                table: "Restaurants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_RestaurantTypeId",
                table: "Restaurants",
                column: "RestaurantTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantTypes_RestaurantTypeId",
                table: "Restaurants",
                column: "RestaurantTypeId",
                principalTable: "RestaurantTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantTypes_RestaurantTypeId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_RestaurantTypeId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "RestaurantTypeId",
                table: "Restaurants");
        }
    }
}
