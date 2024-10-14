using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class changeforeignInProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productFeatures_products_productsId",
                table: "productFeatures");

            migrationBuilder.DropIndex(
                name: "IX_productFeatures_productsId",
                table: "productFeatures");

            migrationBuilder.DropColumn(
                name: "productsId",
                table: "productFeatures");

            migrationBuilder.CreateIndex(
                name: "IX_productFeatures_ProductId",
                table: "productFeatures",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_productFeatures_products_ProductId",
                table: "productFeatures",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productFeatures_products_ProductId",
                table: "productFeatures");

            migrationBuilder.DropIndex(
                name: "IX_productFeatures_ProductId",
                table: "productFeatures");

            migrationBuilder.AddColumn<int>(
                name: "productsId",
                table: "productFeatures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_productFeatures_productsId",
                table: "productFeatures",
                column: "productsId");

            migrationBuilder.AddForeignKey(
                name: "FK_productFeatures_products_productsId",
                table: "productFeatures",
                column: "productsId",
                principalTable: "products",
                principalColumn: "Id");
        }
    }
}
