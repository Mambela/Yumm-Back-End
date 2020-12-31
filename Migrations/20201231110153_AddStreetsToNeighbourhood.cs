using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class AddStreetsToNeighbourhood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NeighbourhoodId",
                table: "Streets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                table: "Streets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StreetsId",
                table: "Streets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streets_NeighbourhoodId",
                table: "Streets",
                column: "NeighbourhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_StreetsId",
                table: "Streets",
                column: "StreetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Streets_Neighbourhoods_NeighbourhoodId",
                table: "Streets",
                column: "NeighbourhoodId",
                principalTable: "Neighbourhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Streets_Streets_StreetsId",
                table: "Streets",
                column: "StreetsId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Streets_Neighbourhoods_NeighbourhoodId",
                table: "Streets");

            migrationBuilder.DropForeignKey(
                name: "FK_Streets_Streets_StreetsId",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Streets_NeighbourhoodId",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Streets_StreetsId",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "NeighbourhoodId",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "StreetsId",
                table: "Streets");
        }
    }
}
