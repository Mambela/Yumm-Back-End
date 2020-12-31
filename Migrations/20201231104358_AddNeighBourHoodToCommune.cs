using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class AddNeighBourHoodToCommune : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommuneId",
                table: "Neighbourhoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Neighbourhoods_CommuneId",
                table: "Neighbourhoods",
                column: "CommuneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Neighbourhoods_Communes_CommuneId",
                table: "Neighbourhoods",
                column: "CommuneId",
                principalTable: "Communes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Neighbourhoods_Communes_CommuneId",
                table: "Neighbourhoods");

            migrationBuilder.DropIndex(
                name: "IX_Neighbourhoods_CommuneId",
                table: "Neighbourhoods");

            migrationBuilder.DropColumn(
                name: "CommuneId",
                table: "Neighbourhoods");
        }
    }
}
