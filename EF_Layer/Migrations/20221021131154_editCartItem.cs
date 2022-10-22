using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogicLayer.Migrations
{
    public partial class editCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemColor");

            migrationBuilder.DropColumn(
                name: "ProductColorId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "CartItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 8,
                column: "ColorName",
                value: "turquoise");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ColorId",
                table: "CartItems",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Colors_ColorId",
                table: "CartItems",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Colors_ColorId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ColorId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "ProductColorId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CartItemColor",
                columns: table => new
                {
                    CartItemsId = table.Column<int>(type: "int", nullable: false),
                    ColorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemColor", x => new { x.CartItemsId, x.ColorsId });
                    table.ForeignKey(
                        name: "FK_CartItemColor_CartItems_CartItemsId",
                        column: x => x.CartItemsId,
                        principalTable: "CartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItemColor_Colors_ColorsId",
                        column: x => x.ColorsId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 8,
                column: "ColorName",
                value: "turquo");

            migrationBuilder.CreateIndex(
                name: "IX_CartItemColor_ColorsId",
                table: "CartItemColor",
                column: "ColorsId");
        }
    }
}
