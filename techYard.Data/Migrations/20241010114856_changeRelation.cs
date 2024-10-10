using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class changeRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productDetailsImages_productDetailsImagesId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_productDetailsImagesId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "productDetailsImagesId",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "productsId",
                table: "productDetailsImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_productDetailsImages_productsId",
                table: "productDetailsImages",
                column: "productsId");

            migrationBuilder.AddForeignKey(
                name: "FK_productDetailsImages_products_productsId",
                table: "productDetailsImages",
                column: "productsId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productDetailsImages_products_productsId",
                table: "productDetailsImages");

            migrationBuilder.DropIndex(
                name: "IX_productDetailsImages_productsId",
                table: "productDetailsImages");

            migrationBuilder.DropColumn(
                name: "productsId",
                table: "productDetailsImages");

            migrationBuilder.AddColumn<int>(
                name: "productDetailsImagesId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_productDetailsImagesId",
                table: "products",
                column: "productDetailsImagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productDetailsImages_productDetailsImagesId",
                table: "products",
                column: "productDetailsImagesId",
                principalTable: "productDetailsImages",
                principalColumn: "Id");
        }
    }
}
