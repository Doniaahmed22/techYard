using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using techYard.API.Controllers;
using techYard.Service.Services.featuresServices.Dtos;
using techYard.Service.Services.featuresServices;
using techYard.Service.Services.CartServices;
using techYard.Service.Services.CartServices.Dtos;
using techYard.Service.Services.AccountServices;
using techYard.Service.Services.productsServices;

namespace techYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        readonly ICartService _cartService;
        readonly IAccountService _accountService;
        readonly IProductServices _productServices;
        public CartController(ICartService cartService, IAccountService accountService, IProductServices productServices)
        {
            _productServices = productServices;
            _accountService = accountService;
            _cartService = cartService;
        }


        [HttpGet]
        [Route("GetAllProductsInCart")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<GetCart>>> GetAllProductsInCart()
        {
            var products = await _cartService.GetAllProductsInCart();
            return Ok(products);
        }




        [HttpGet]
        [Route("GetProductInCartById/{id}")]
        //[Authorize]
        public async Task<ActionResult> GetProductInCartById(int id)
        {
            var product = await _cartService.GetProductInCartByCartId(id);
            if (product == null)
            {
                return NotFound("invalid Id");
            }
            return Ok(product);
        }




        [HttpDelete("DeleteProductFromCartById/{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteProductFromCartById(int id)
        {
            var feature = await _cartService.DeleteProductFromCartById(id);
            if (feature == null)
            {
                return NotFound("Invalid Id");
            }
            return Ok();
        }




        [HttpPut]
        [Route("UpdateProductInCart")]
        //[Authorize]
        public async Task<IActionResult> UpdateProductInCart(int id, AddCart cartDto)
        {
            if (cartDto == null)
            {
                return BadRequest("Product in Cart is Empty");
            }

            var user = await _accountService.GetUserById(cartDto.UserId);
            if (user == null)
            {
                return NotFound("User Not found");
            }

            var product = await _productServices.GetProductById(cartDto.ProductId);
            if (product == null)
            {
                return NotFound("Product Not found");
            }

            if (cartDto.Quantity <= 0)
            {
                return BadRequest("quantity Must be positive number");
            }

            var productInCart = await _cartService.UpdateProductInCart(id, cartDto);
            if (productInCart == null)
            {
                return NotFound("Invalid Id");
            }
            return Ok();
        }





        [HttpPost]
        [Route("AddProductInCart")]
        //[Authorize]
        public async Task<ActionResult<GetCart>> AddProductInCart(AddCart cartDto)
        {
            if (cartDto == null)
            {
                return BadRequest("Product in cart is empty");
            }

            var user = await _accountService.GetUserById(cartDto.UserId);
            if (user == null)
            {
                return NotFound("User Not found");
            }

            var product = await _productServices.GetProductById(cartDto.ProductId);
            if (product == null)
            {
                return NotFound("Product Not found");
            }

            if(cartDto.Quantity <= 0)
            {
                return BadRequest("quantity Must be positive number");
            }

            var isExist = await _cartService.IsProductAndUserAlreadyExist(cartDto.ProductId, cartDto.UserId);
            if (isExist != null)
            {
                //var cart = await _cartService.GetProductInCartById(isExist);
                cartDto.Quantity += isExist.Quantity;
                await _cartService.UpdateProductInCart(isExist.Id,cartDto);

                var updatedCart = new GetCart
                {
                    Id = isExist.Id,
                    ProductId = cartDto.ProductId,
                    UserId = cartDto.UserId,
                    Quantity = cartDto.Quantity,
                };

                return Ok(updatedCart);
            }

            var productInCart = await _cartService.AddProductInCart(cartDto);

            // إرجاع النتيجة مع حالة 200 OK
            return Ok(productInCart);
        }






        [HttpGet]
        [Route("GetCartsByUserId/{UserId}")]
        //[Authorize]
        public async Task<ActionResult> GetCartsByUserId(string UserId)
        {
            var products = await _cartService.GetProductInCartByUserId(UserId);
            if (products == null)
            {
                return NotFound("there are no carts for this user yet");
            }
            return Ok(products);
        }






        [HttpGet]
        [Route("GetAllProductsFromTheCart")]
        //[Authorize]
        public async Task<ActionResult> GetAllProductsFromTheCart()
        {
            var products = await _cartService.GetAllProductsFromTheCart();
            if (products == null)
            {
                return NotFound("there are no products added to any cart yet");
            }
            return Ok(products);
        }






        [HttpGet]
        [Route("GetAllUsersFromTheCart")]
        //[Authorize]
        public async Task<ActionResult> GetAllUsersFromTheCart()
        {
            var products = await _cartService.GetAllUsersFromTheCart();
            if (products == null)
            {
                return NotFound("there are no products added to any cart yet");
            }
            return Ok(products);
        }








        [HttpGet]
        [Route("GetProductsByEachUser")]
        //[Authorize]
        public async Task<ActionResult> GetProductsByEachUser()
        {
            var products = await _cartService.GetUsersWithProductsAsync();
            if (products == null)
            {
                return NotFound("there are no carts yet");
            }
            return Ok(products);
        }


    }
}

