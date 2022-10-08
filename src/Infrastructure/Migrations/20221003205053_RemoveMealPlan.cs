using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dietonator.Infrastructure.Migrations
{
    public partial class RemoveMealPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_meals_mealplans_mealplanid",
                table: "meals");

            migrationBuilder.DropTable(
                name: "mealplans");

            migrationBuilder.DropIndex(
                name: "ix_meals_mealplanid",
                table: "meals");

            migrationBuilder.DropColumn(
                name: "mealplanid",
                table: "meals");

            migrationBuilder.AddColumn<DateOnly>(
                name: "fordate",
                table: "meals",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<Guid>(
                name: "userid",
                table: "meals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fordate",
                table: "meals");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "meals");

            migrationBuilder.AddColumn<Guid>(
                name: "mealplanid",
                table: "meals",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mealplans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: true),
                    fordate = table.Column<DateOnly>(type: "date", nullable: false),
                    foruser = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mealplans", x => x.id);
                });

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
    }
}
