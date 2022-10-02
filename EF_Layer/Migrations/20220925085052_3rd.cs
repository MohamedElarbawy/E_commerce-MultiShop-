using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogicLayer.Migrations
{
    public partial class _3rd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "Category");
        }
    }
}
