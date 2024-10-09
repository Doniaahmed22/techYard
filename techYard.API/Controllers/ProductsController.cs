using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using techYard.Service.Services.productsServices;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductServices _productServices;
        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<getProduct>>> GetAllProducts()
        {
            var Products = await _productServices.GetAllProducts();
            return Ok(Products);
        }

        [HttpGet]
        [Route("GetProductById/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<getProduct>> GetProductById(int id)
        {
            var Product = await _productServices.GetProductById(id);
            return Ok(Product);
        }

    }
}
