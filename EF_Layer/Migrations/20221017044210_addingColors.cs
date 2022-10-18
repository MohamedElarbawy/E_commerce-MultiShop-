using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogicLayer.Migrations
{
    public partial class addingColors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "e8b0fc1c-a828-4fa6-b27b-563c9b3de76bMultiShop",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "1832c645-666c-4b78-9bc3-f512fc6e6364MultiShop");

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "ColorName" },
                values: new object[,]
                {
                    { 1, "black" },
                    { 2, "white" },
                    { 3, "red" },
                    { 4, "green" },
                    { 5, "blue" },
                    { 6, "magent" },
                    { 7, "cyan" },
                    { 8, "turquo" },
                    { 9, "brown" },
                    { 10, "grey" },
                    { 11, "beige" },
                    { 12, "pink" },
                    { 13, "purple" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "1832c645-666c-4b78-9bc3-f512fc6e6364MultiShop",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "e8b0fc1c-a828-4fa6-b27b-563c9b3de76bMultiShop");
        }
    }
}
