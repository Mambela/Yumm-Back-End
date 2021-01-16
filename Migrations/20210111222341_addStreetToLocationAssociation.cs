using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class addStreetToLocationAssociation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_StreetId",
                table: "Locations",
                column: "StreetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Streets_StreetId",
                table: "Locations",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Streets_StreetId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_StreetId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "Locations");
        }
    }
}
