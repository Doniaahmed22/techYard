using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class changeForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoriesId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "products",
                newName: "categoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoriesId",
                table: "products",
                newName: "IX_products_categoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_categoriesId",
                table: "products",
                column: "categoriesId",
                principalTable: "categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_categoriesId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "categoriesId",
                table: "products",
                newName: "CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_products_categoriesId",
                table: "products",
                newName: "IX_products_CategoriesId");

            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoriesId",
                table: "products",
                column: "CategoriesId",
                principalTable: "categories",
                principalColumn: "Id");
        }
    }
}
