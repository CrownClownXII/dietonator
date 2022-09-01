using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dietonator.Infrastructure.Migrations
{
    public partial class AddingMealProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mealmealplan_mealplan_mealplansid",
                table: "mealmealplan");

            migrationBuilder.DropTable(
                name: "mealproduct");

            migrationBuilder.DropPrimaryKey(
                name: "pk_mealplan",
                table: "mealplan");

            migrationBuilder.RenameTable(
                name: "mealplan",
                newName: "mealplans");

            migrationBuilder.AddPrimaryKey(
                name: "pk_mealplans",
                table: "mealplans",
                column: "id");

            migrationBuilder.CreateTable(
                name: "mealproducts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    productid = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    amounttype = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: true),
                    lastmodified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mealproducts", x => x.id);
                    table.ForeignKey(
                        name: "fk_mealproducts_products_productid",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_mealproducts_productid",
                table: "mealproducts",
                column: "productid");

            migrationBuilder.AddForeignKey(
                name: "fk_mealmealplan_mealplans_mealplansid",
                table: "mealmealplan",
                column: "mealplansid",
                principalTable: "mealplans",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mealmealplan_mealplans_mealplansid",
                table: "mealmealplan");

            migrationBuilder.DropTable(
                name: "mealmealproduct");

            migrationBuilder.DropTable(
                name: "mealproducts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_mealplans",
                table: "mealplans");

            migrationBuilder.RenameTable(
                name: "mealplans",
                newName: "mealplan");

            migrationBuilder.AddPrimaryKey(
                name: "pk_mealplan",
                table: "mealplan",
                column: "id");

            migrationBuilder.CreateTable(
                name: "mealproduct",
                columns: table => new
                {
                    mealsid = table.Column<Guid>(type: "uuid", nullable: false),
                    productsid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mealproduct", x => new { x.mealsid, x.productsid });
                    table.ForeignKey(
                        name: "fk_mealproduct_meals_mealsid",
                        column: x => x.mealsid,
                        principalTable: "meals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mealproduct_products_productsid",
                        column: x => x.productsid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_mealproduct_productsid",
                table: "mealproduct",
                column: "productsid");

            migrationBuilder.AddForeignKey(
                name: "fk_mealmealplan_mealplan_mealplansid",
                table: "mealmealplan",
                column: "mealplansid",
                principalTable: "mealplan",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
