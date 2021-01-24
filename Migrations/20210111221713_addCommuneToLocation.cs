using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class addCommuneToLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommuneId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CommuneId",
                table: "Locations",
                column: "CommuneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Communes_CommuneId",
                table: "Locations",
                column: "CommuneId",
                principalTable: "Communes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Communes_CommuneId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CommuneId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CommuneId",
                table: "Locations");
        }
    }
}
