using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitLobieDesign.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCategoryPriceAddTotalPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Kits_KitId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "KitId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Kits_KitId",
                table: "Categories",
                column: "KitId",
                principalTable: "Kits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Kits_KitId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "KitId",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Categories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Kits_KitId",
                table: "Categories",
                column: "KitId",
                principalTable: "Kits",
                principalColumn: "Id");
        }
    }
}
