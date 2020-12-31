using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class AddCommuneToCityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Communes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Communes_CityId",
                table: "Communes",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Communes_Cities_CityId",
                table: "Communes",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communes_Cities_CityId",
                table: "Communes");

            migrationBuilder.DropIndex(
                name: "IX_Communes_CityId",
                table: "Communes");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Communes");
        }
    }
}
