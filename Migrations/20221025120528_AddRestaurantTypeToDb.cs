using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yumm_Back_End.Migrations
{
    public partial class AddRestaurantTypeToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantTypeId",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RestaurantTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_RestaurantTypeId",
                table: "Restaurants",
                column: "RestaurantTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantTypes_RestaurantTypeId",
                table: "Restaurants",
                column: "RestaurantTypeId",
                principalTable: "RestaurantTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantTypes_RestaurantTypeId",
                table: "Restaurants");

            migrationBuilder.DropTable(
                name: "RestaurantTypes");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_RestaurantTypeId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "RestaurantTypeId",
                table: "Restaurants");
        }
    }
}
