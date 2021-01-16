using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class addHoodToLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NeighbourhoodId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_NeighbourhoodId",
                table: "Locations",
                column: "NeighbourhoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Neighbourhoods_NeighbourhoodId",
                table: "Locations",
                column: "NeighbourhoodId",
                principalTable: "Neighbourhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Neighbourhoods_NeighbourhoodId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_NeighbourhoodId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "NeighbourhoodId",
                table: "Locations");
        }
    }
}
