using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class AddProductsToMenuGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuGroupId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_MenuGroupId",
                table: "Products",
                column: "MenuGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MenuGroups_MenuGroupId",
                table: "Products",
                column: "MenuGroupId",
                principalTable: "MenuGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_MenuGroups_MenuGroupId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MenuGroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MenuGroupId",
                table: "Products");
        }
    }
}
