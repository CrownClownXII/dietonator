using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dietonator.Infrastructure.Migrations
{
    public partial class ChangeMealToMealPlanToOneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mealmealplan");

            migrationBuilder.AddColumn<Guid>(
                name: "mealplanid",
                table: "meals",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_meals_mealplanid",
                table: "meals",
                column: "mealplanid");

            migrationBuilder.AddForeignKey(
                name: "fk_meals_mealplans_mealplanid",
                table: "meals",
                column: "mealplanid",
                principalTable: "mealplans",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_meals_mealplans_mealplanid",
                table: "meals");

            migrationBuilder.DropIndex(
                name: "ix_meals_mealplanid",
                table: "meals");

            migrationBuilder.DropColumn(
                name: "mealplanid",
                table: "meals");

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
                        name: "fk_mealmealplan_mealplans_mealplansid",
                        column: x => x.mealplansid,
                        principalTable: "mealplans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mealmealplan_meals_mealsid",
                        column: x => x.mealsid,
                        principalTable: "meals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_mealmealplan_mealsid",
                table: "mealmealplan",
                column: "mealsid");
        }
    }
}
