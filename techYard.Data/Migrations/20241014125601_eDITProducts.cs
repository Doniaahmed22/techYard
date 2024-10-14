using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class eDITProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productDetailsImages_products_productsId",
                table: "productDetailsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_productFeatures_products_ProductsId",
                table: "productFeatures");

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
                table: "productFeatures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_productFeatures_products_ProductsId",
                table: "productFeatures",
                column: "ProductsId",
                principalTable: "products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productDetailsImages_products_ProductsId",
                table: "productDetailsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_productFeatures_products_ProductsId",
                table: "productFeatures");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "productDetailsImages",
                newName: "productsId");

            migrationBuilder.RenameIndex(
                name: "IX_productDetailsImages_ProductsId",
                table: "productDetailsImages",
                newName: "IX_productDetailsImages_productsId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductsId",
                table: "productFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "productsId",
                table: "productDetailsImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_productDetailsImages_products_productsId",
                table: "productDetailsImages",
                column: "productsId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productFeatures_products_ProductsId",
                table: "productFeatures",
                column: "ProductsId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
