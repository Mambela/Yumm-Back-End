using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class addOrderCollectedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CollectedByUserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CollectedByUserId",
                table: "Orders",
                column: "CollectedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CollectedByUserId",
                table: "Orders",
                column: "CollectedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CollectedByUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CollectedByUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CollectedByUserId",
                table: "Orders");
        }
    }
}
