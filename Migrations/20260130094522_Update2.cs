using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_Warehouse_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PickingInfos_PicklistItems_PickListItemId",
                table: "PickingInfos");

            migrationBuilder.RenameColumn(
                name: "PickListItemId",
                table: "PickingInfos",
                newName: "PicklistItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PickingInfos_PickListItemId",
                table: "PickingInfos",
                newName: "IX_PickingInfos_PicklistItemId");

            migrationBuilder.AlterColumn<long>(
                name: "GrnItemId",
                table: "CheckingInfos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PickingInfos_PicklistItems_PicklistItemId",
                table: "PickingInfos",
                column: "PicklistItemId",
                principalTable: "PicklistItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PickingInfos_PicklistItems_PicklistItemId",
                table: "PickingInfos");

            migrationBuilder.RenameColumn(
                name: "PicklistItemId",
                table: "PickingInfos",
                newName: "PickListItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PickingInfos_PicklistItemId",
                table: "PickingInfos",
                newName: "IX_PickingInfos_PickListItemId");

            migrationBuilder.AlterColumn<long>(
                name: "GrnItemId",
                table: "CheckingInfos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_PickingInfos_PicklistItems_PickListItemId",
                table: "PickingInfos",
                column: "PickListItemId",
                principalTable: "PicklistItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
