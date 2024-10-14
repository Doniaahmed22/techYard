using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class changeforeignInProductsImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productDetailsImages_products_ProductsId",
                table: "productDetailsImages");

            migrationBuilder.DropIndex(
                name: "IX_productDetailsImages_ProductsId",
                table: "productDetailsImages");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "productDetailsImages");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "productDetailsImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_productDetailsImages_ProductId",
                table: "productDetailsImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_productDetailsImages_products_ProductId",
                table: "productDetailsImages",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productDetailsImages_products_ProductId",
                table: "productDetailsImages");

            migrationBuilder.DropIndex(
                name: "IX_productDetailsImages_ProductId",
                table: "productDetailsImages");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "productDetailsImages");

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "productDetailsImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_productDetailsImages_ProductsId",
                table: "productDetailsImages",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_productDetailsImages_products_ProductsId",
                table: "productDetailsImages",
                column: "ProductsId",
                principalTable: "products",
                principalColumn: "Id");
        }
    }
}
