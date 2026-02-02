using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_Warehouse_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePicklisttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Picklists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Picklists");
        }
    }
}
