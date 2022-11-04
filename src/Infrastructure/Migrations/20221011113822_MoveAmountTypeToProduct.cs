using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dietonator.Infrastructure.Migrations
{
    public partial class MoveAmountTypeToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amounttype",
                table: "mealproducts");

            migrationBuilder.AddColumn<int>(
                name: "amounttype",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amounttype",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "amounttype",
                table: "mealproducts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
