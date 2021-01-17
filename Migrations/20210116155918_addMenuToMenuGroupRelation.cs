using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class addMenuToMenuGroupRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "MenuGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuGroups_MenuId",
                table: "MenuGroups",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuGroups_Menus_MenuId",
                table: "MenuGroups",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuGroups_Menus_MenuId",
                table: "MenuGroups");

            migrationBuilder.DropIndex(
                name: "IX_MenuGroups_MenuId",
                table: "MenuGroups");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "MenuGroups");
        }
    }
}
