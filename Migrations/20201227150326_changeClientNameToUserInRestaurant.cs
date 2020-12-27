using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class changeClientNameToUserInRestaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_ClientId",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Restaurants",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_ClientId",
                table: "Restaurants",
                newName: "IX_Restaurants_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_UserId",
                table: "Restaurants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_UserId",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Restaurants",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_UserId",
                table: "Restaurants",
                newName: "IX_Restaurants_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_ClientId",
                table: "Restaurants",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
