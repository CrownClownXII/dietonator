using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dietonator.Infrastructure.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mealplan",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fordate = table.Column<DateOnly>(type: "date", nullable: false),
                    foruser = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: true),
                    lastmodified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mealplan", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "meals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: true),
                    lastmodified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_meals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    kcal = table.Column<int>(type: "integer", nullable: false),
                    proteins = table.Column<float>(type: "real", nullable: false),
                    fats = table.Column<float>(type: "real", nullable: false),
                    carbohydrates = table.Column<float>(type: "real", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: true),
                    lastmodified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mealmealplan",
                columns: table => new
                {
                    mealplansid = table.Column<Guid>(type: "uuid", nullable: false),
                    mealsid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mealmealplan", x => new { x.mealplansid, x.mealsid });
                    table.ForeignKey(
                        name: "fk_mealmealplan_mealplan_mealplansid",
                        column: x => x.mealplansid,
                        principalTable: "mealplan",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mealmealplan_meals_mealsid",
                        column: x => x.mealsid,
                        principalTable: "meals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "ix_mealmealplan_mealsid",
                table: "mealmealplan",
                column: "mealsid");

            migrationBuilder.CreateIndex(
                name: "ix_mealproduct_productsid",
                table: "mealproduct",
                column: "productsid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mealmealplan");

            migrationBuilder.DropTable(
                name: "mealproduct");

            migrationBuilder.DropTable(
                name: "mealplan");

            migrationBuilder.DropTable(
                name: "meals");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
