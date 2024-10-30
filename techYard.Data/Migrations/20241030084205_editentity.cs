using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class editentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productDetailsImages_products_ProductsId",
                schema: "dbo",
                table: "productDetailsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_productFeatures_products_ProductsId",
                schema: "dbo",
                table: "productFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_categoriesId",
                schema: "dbo",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_productsInCart_products_ProductId",
                schema: "dbo",
                table: "productsInCart");

            migrationBuilder.DropForeignKey(
                name: "FK_productsInCart_Users_UserId",
                schema: "dbo",
                table: "productsInCart");

            migrationBuilder.DropIndex(
                name: "IX_productsInCart_UserId",
                schema: "dbo",
                table: "productsInCart");

            migrationBuilder.DropIndex(
                name: "IX_productDetailsImages_ProductsId",
                schema: "dbo",
                table: "productDetailsImages");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "productsInCart");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                schema: "dbo",
                table: "productDetailsImages");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                schema: "dbo",
                table: "productsInCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductsId",
                schema: "dbo",
                table: "productFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "dbo",
                table: "productDetailsImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "carts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productsInCart_CartId",
                schema: "dbo",
                table: "productsInCart",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_productDetailsImages_ProductId",
                schema: "dbo",
                table: "productDetailsImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_carts_UserId",
                schema: "dbo",
                table: "carts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_productDetailsImages_products_ProductId",
                schema: "dbo",
                table: "productDetailsImages",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productFeatures_products_ProductsId",
                schema: "dbo",
                table: "productFeatures",
                column: "ProductsId",
                principalSchema: "dbo",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_categoriesId",
                schema: "dbo",
                table: "products",
                column: "categoriesId",
                principalSchema: "dbo",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productsInCart_carts_CartId",
                schema: "dbo",
                table: "productsInCart",
                column: "CartId",
                principalSchema: "dbo",
                principalTable: "carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productsInCart_products_ProductId",
                schema: "dbo",
                table: "productsInCart",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productDetailsImages_products_ProductId",
                schema: "dbo",
                table: "productDetailsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_productFeatures_products_ProductsId",
                schema: "dbo",
                table: "productFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_categoriesId",
                schema: "dbo",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_productsInCart_carts_CartId",
                schema: "dbo",
                table: "productsInCart");

            migrationBuilder.DropForeignKey(
                name: "FK_productsInCart_products_ProductId",
                schema: "dbo",
                table: "productsInCart");

            migrationBuilder.DropTable(
                name: "carts",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_productsInCart_CartId",
                schema: "dbo",
                table: "productsInCart");

            migrationBuilder.DropIndex(
                name: "IX_productDetailsImages_ProductId",
                schema: "dbo",
                table: "productDetailsImages");

            migrationBuilder.DropColumn(
                name: "CartId",
                schema: "dbo",
                table: "productsInCart");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "dbo",
                table: "productDetailsImages");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "productsInCart",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ProductsId",
                schema: "dbo",
                table: "productFeatures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                schema: "dbo",
                table: "productDetailsImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_productsInCart_UserId",
                schema: "dbo",
                table: "productsInCart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_productDetailsImages_ProductsId",
                schema: "dbo",
                table: "productDetailsImages",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_productDetailsImages_products_ProductsId",
                schema: "dbo",
                table: "productDetailsImages",
                column: "ProductsId",
                principalSchema: "dbo",
                principalTable: "products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_productFeatures_products_ProductsId",
                schema: "dbo",
                table: "productFeatures",
                column: "ProductsId",
                principalSchema: "dbo",
                principalTable: "products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_categoriesId",
                schema: "dbo",
                table: "products",
                column: "categoriesId",
                principalSchema: "dbo",
                principalTable: "categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_productsInCart_products_ProductId",
                schema: "dbo",
                table: "productsInCart",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productsInCart_Users_UserId",
                schema: "dbo",
                table: "productsInCart",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
