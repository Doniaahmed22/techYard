using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class editproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productDetailsImages_products_productsId",
                table: "productDetailsImages");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "productDetailsImages");

            migrationBuilder.RenameColumn(
                name: "productsId",
                table: "productDetailsImages",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_productDetailsImages_productsId",
                table: "productDetailsImages",
                newName: "IX_productDetailsImages_ProductsId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductsId",
                table: "productDetailsImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_productDetailsImages_products_ProductsId",
                table: "productDetailsImages",
                column: "ProductsId",
                principalTable: "products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productDetailsImages_products_ProductsId",
                table: "productDetailsImages");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "productDetailsImages",
                newName: "productsId");

            migrationBuilder.RenameIndex(
                name: "IX_productDetailsImages_ProductsId",
                table: "productDetailsImages",
                newName: "IX_productDetailsImages_productsId");

            migrationBuilder.AlterColumn<int>(
                name: "productsId",
                table: "productDetailsImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "productDetailsImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_productDetailsImages_products_productsId",
                table: "productDetailsImages",
                column: "productsId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
