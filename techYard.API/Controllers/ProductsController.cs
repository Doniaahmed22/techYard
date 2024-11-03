using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using techYard.Data.Entities;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.featuresServices.Dtos;
using techYard.Service.Services.ProductImagesServices;
using techYard.Service.Services.productsServices;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductServices _productServices;
        readonly IProductDetailsImagesServices productDetailsImagesServices;
        public ProductsController(IProductServices productServices , IProductDetailsImagesServices _productDetailsImagesServices)
        {
            _productServices = productServices;
            productDetailsImagesServices = _productDetailsImagesServices;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productServices.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("GetProductById/{id}")]
        public async Task<ActionResult<getProduct>> GetProductById(int id)
        {
            var product = await _productServices.GetProductByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto productDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdProduct = await _productServices.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedProduct = await _productServices.UpdateProductAsync(id, productDto);
            return updatedProduct == null ? NotFound() : Ok(updatedProduct);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productServices.DeleteProductAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("Laptops")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetLaptops()
        {
            var laptops = await _productServices.GetLaptopsAsync();
            return Ok(laptops);
        }

        [HttpGet("Desktops")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetDesktops()
        {
            var desktops = await _productServices.GetDesktopsAsync();
            return Ok(desktops);
        }

        [HttpGet("Accessories")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAccessories()
        {
            var accessories = await _productServices.GetAccessoriesAsync();
            return Ok(accessories);
        }
    }
}
