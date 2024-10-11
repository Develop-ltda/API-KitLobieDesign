using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitLobieDesign.Migrations
{
    /// <inheritdoc />
    public partial class CategoryPriceAddTotalPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Categories",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Categories");
        }
    }
}
