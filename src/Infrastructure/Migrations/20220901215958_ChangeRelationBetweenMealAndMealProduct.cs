using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dietonator.Infrastructure.Migrations
{
    public partial class ChangeRelationBetweenMealAndMealProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mealmealproduct");

            migrationBuilder.AddColumn<Guid>(
                name: "mealid",
                table: "mealproducts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_mealproducts_mealid",
                table: "mealproducts",
                column: "mealid");

            migrationBuilder.AddForeignKey(
                name: "fk_mealproducts_meals_mealid",
                table: "mealproducts",
                column: "mealid",
                principalTable: "meals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mealproducts_meals_mealid",
                table: "mealproducts");

            migrationBuilder.DropIndex(
                name: "ix_mealproducts_mealid",
                table: "mealproducts");

            migrationBuilder.DropColumn(
                name: "mealid",
                table: "mealproducts");

            migrationBuilder.CreateTable(
                name: "mealmealproduct",
                columns: table => new
                {
                    mealsid = table.Column<Guid>(type: "uuid", nullable: false),
                    productsid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mealmealproduct", x => new { x.mealsid, x.productsid });
                    table.ForeignKey(
                        name: "fk_mealmealproduct_mealproducts_productsid",
                        column: x => x.productsid,
                        principalTable: "mealproducts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mealmealproduct_meals_mealsid",
                        column: x => x.mealsid,
                        principalTable: "meals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_mealmealproduct_productsid",
                table: "mealmealproduct",
                column: "productsid");
        }
    }
}
