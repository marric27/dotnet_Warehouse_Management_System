using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_Warehouse_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "PicklistItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "PicklistItems");
        }
    }
}
