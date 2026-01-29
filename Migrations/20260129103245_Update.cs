using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_Warehouse_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckingInfos_StockUnits_StockUnitId",
                table: "CheckingInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckingInfos_StockUnits_StockUnitId",
                table: "CheckingInfos",
                column: "StockUnitId",
                principalTable: "StockUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckingInfos_StockUnits_StockUnitId",
                table: "CheckingInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckingInfos_StockUnits_StockUnitId",
                table: "CheckingInfos",
                column: "StockUnitId",
                principalTable: "StockUnits",
                principalColumn: "Id");
        }
    }
}
