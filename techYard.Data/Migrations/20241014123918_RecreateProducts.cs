using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class RecreateProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageUrlInHover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    oldPrice = table.Column<double>(type: "float", nullable: true),
                    discount = table.Column<int>(type: "int", nullable: true),
                    soldOut = table.Column<bool>(type: "bit", nullable: true),
                    popular = table.Column<bool>(type: "bit", nullable: true),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoriesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_categories_categoriesId",
                        column: x => x.categoriesId,
                        principalTable: "categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "productDetailsImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productDetailsImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productDetailsImages_products_productsId",
                        column: x => x.productsId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    processor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    graphicCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    storage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ramSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ramType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dimensions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productFeatures_products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productDetailsImages_productsId",
                table: "productDetailsImages",
                column: "productsId");

            migrationBuilder.CreateIndex(
                name: "IX_productFeatures_ProductsId",
                table: "productFeatures",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_products_categoriesId",
                table: "products",
                column: "categoriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productDetailsImages");

            migrationBuilder.DropTable(
                name: "productFeatures");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
