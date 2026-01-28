using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_Warehouse_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStockUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockUnits_Products_ProductId",
                table: "StockUnits");

            migrationBuilder.DropIndex(
                name: "IX_StockUnits_ProductId",
                table: "StockUnits");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "StockUnits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "StockUnits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_StockUnits_ProductId",
                table: "StockUnits",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockUnits_Products_ProductId",
                table: "StockUnits",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
