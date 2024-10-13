using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class changeInFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productFeatures_productFeaturesId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_productFeaturesId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "products");

            migrationBuilder.DropColumn(
                name: "productFeaturesId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "OS",
                table: "productFeatures");

            migrationBuilder.RenameColumn(
                name: "model",
                table: "productFeatures",
                newName: "ScreenSize");

            migrationBuilder.AddColumn<string>(
                name: "OS",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "model",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "productFeatures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "productsId",
                table: "productFeatures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "productDetailsImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productFeatures_products_productsId",
                table: "productFeatures");

            migrationBuilder.DropIndex(
                name: "IX_productFeatures_productsId",
                table: "productFeatures");

            migrationBuilder.DropColumn(
                name: "OS",
                table: "products");

            migrationBuilder.DropColumn(
                name: "model",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "productFeatures");

            migrationBuilder.DropColumn(
                name: "productsId",
                table: "productFeatures");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "productDetailsImages");

            migrationBuilder.RenameColumn(
                name: "ScreenSize",
                table: "productFeatures",
                newName: "model");

            migrationBuilder.AddColumn<double>(
                name: "NewPrice",
                table: "products",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "productFeaturesId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OS",
                table: "productFeatures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_productFeaturesId",
                table: "products",
                column: "productFeaturesId",
                unique: true,
                filter: "[productFeaturesId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productFeatures_productFeaturesId",
                table: "products",
                column: "productFeaturesId",
                principalTable: "productFeatures",
                principalColumn: "Id");
        }
    }
}
