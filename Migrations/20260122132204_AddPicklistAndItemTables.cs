using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_Warehouse_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddPicklistAndItemTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Slots",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Picklists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReleaseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picklists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PicklistItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    PickedQty = table.Column<int>(type: "int", nullable: false),
                    PickingSequence = table.Column<int>(type: "int", nullable: false),
                    ErrorReason = table.Column<int>(type: "int", nullable: false),
                    SlotCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderLineNumber = table.Column<int>(type: "int", nullable: false),
                    PicklistId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicklistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PicklistItems_Picklists_PicklistId",
                        column: x => x.PicklistId,
                        principalTable: "Picklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slots_ProductId",
                table: "Slots",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PicklistItems_PicklistId",
                table: "PicklistItems",
                column: "PicklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Picklists_Code",
                table: "Picklists",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Products_ProductId",
                table: "Slots",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Products_ProductId",
                table: "Slots");

            migrationBuilder.DropTable(
                name: "PicklistItems");

            migrationBuilder.DropTable(
                name: "Picklists");

            migrationBuilder.DropIndex(
                name: "IX_Slots_ProductId",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Slots");
        }
    }
}
