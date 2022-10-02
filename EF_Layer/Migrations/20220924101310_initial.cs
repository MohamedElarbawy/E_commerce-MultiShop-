using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogicLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductPrice = table.Column<double>(type: "float", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    productSize = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductColorId = table.Column<int>(type: "int", nullable: false),
                    ProductCaregoryId = table.Column<int>(type: "int", nullable: false),
                    productLastUpdate = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    productStock = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories",
                        column: x => x.ProductCaregoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Colors",
                        column: x => x.ProductColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCaregoryId",
                table: "Products",
                column: "ProductCaregoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductColorId",
                table: "Products",
                column: "ProductColorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Colors");
        }
    }
}
