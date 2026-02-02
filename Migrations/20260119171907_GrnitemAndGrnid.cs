using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_Warehouse_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class GrnitemAndGrnid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrnItems_Grns_GrnId",
                table: "GrnItems");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Grns",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "GrnId",
                table: "GrnItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "GrnItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Grns_Code",
                table: "Grns",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GrnItems_Code",
                table: "GrnItems",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GrnItems_Grns_GrnId",
                table: "GrnItems",
                column: "GrnId",
                principalTable: "Grns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrnItems_Grns_GrnId",
                table: "GrnItems");

            migrationBuilder.DropIndex(
                name: "IX_Grns_Code",
                table: "Grns");

            migrationBuilder.DropIndex(
                name: "IX_GrnItems_Code",
                table: "GrnItems");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Grns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<long>(
                name: "GrnId",
                table: "GrnItems",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "GrnItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_GrnItems_Grns_GrnId",
                table: "GrnItems",
                column: "GrnId",
                principalTable: "Grns",
                principalColumn: "Id");
        }
    }
}
