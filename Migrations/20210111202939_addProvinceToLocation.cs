using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class addProvinceToLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ProvinceId",
                table: "Locations",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Provinces_ProvinceId",
                table: "Locations",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Provinces_ProvinceId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_ProvinceId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Locations");
        }
    }
}
