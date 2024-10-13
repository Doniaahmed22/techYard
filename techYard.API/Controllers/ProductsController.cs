﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using techYard.Data.Entities;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.featuresServices.Dtos;
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



        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return NotFound("Invalid Id");
            }

            var product = await _productServices.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            try
            {
                // التحقق من أن المسار المخزن في قاعدة البيانات نسبي
                string CleanPath(string path)
                {
                    if (path.StartsWith("http"))
                    {
                        // استخراج الجزء النسبي فقط من المسار
                        var uri = new Uri(path);
                        return uri.AbsolutePath.TrimStart('/');
                    }
                    return path;
                }

                // حذف الصورة الرئيسية
                var mainImagePath = CleanPath(product.imageUrl);
                var fullMainImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", mainImagePath);
                if (System.IO.File.Exists(fullMainImagePath))
                {
                    System.IO.File.Delete(fullMainImagePath);
                }

                // حذف صورة الهوفر
                var hoverImagePath = CleanPath(product.imageUrlInHover);
                var fullHoverImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", hoverImagePath);
                if (System.IO.File.Exists(fullHoverImagePath))
                {
                    System.IO.File.Delete(fullHoverImagePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting images: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the product images.");
            }

            await _productServices.DeleteProduct(id);
            return Ok($"Product with ID {id} deleted successfully.");
        }



        [HttpPost]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] productForAdditionDto productDto)
        {
            // تحقق من وجود المنتج أولاً
            var existingProduct = await _productServices.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            // التأكد من وجود المجلد wwwroot/Images
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/products/");

            // تأكد من إنشاء المجلد إذا لم يكن موجودًا
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            // التعامل مع الصورة الرئيسية
            if (productDto.ImageUrl != null && productDto.ImageUrl.Length > 0)
            {
                // معالجة الصورة الرئيسية
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.ImageUrl.FileName);
                var fullPath = Path.Combine(imagePath, uniqueFileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await productDto.ImageUrl.CopyToAsync(stream);
                }

                existingProduct.imageUrl = "Images/products/" + uniqueFileName; // مسار نسبي للصورة الرئيسية
            }
            else if (string.IsNullOrEmpty(existingProduct.imageUrl))
            {
                // إذا لم يتم تقديم صورة جديدة، نحتفظ بالصورة القديمة فقط إذا لم تكن موجودة
                return BadRequest("Please provide a main image or ensure the existing image is available.");
            }

            // التعامل مع صورة hover
            if (productDto.ImageUrlHover != null && productDto.ImageUrlHover.Length > 0)
            {
                // معالجة الصورة المتداخلة (Hover)
                var uniqueHoverFileName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.ImageUrlHover.FileName);
                var hoverFullPath = Path.Combine(imagePath, uniqueHoverFileName);

                using (var stream = new FileStream(hoverFullPath, FileMode.Create))
                {
                    await productDto.ImageUrlHover.CopyToAsync(stream);
                }

                existingProduct.imageUrlInHover = "Images/products/" + uniqueHoverFileName; // مسار نسبي لصورة الـ hover
            }
            else if (string.IsNullOrEmpty(existingProduct.imageUrlInHover))
            {
                // إذا لم يتم تقديم صورة جديدة، نحتفظ بالصورة القديمة فقط إذا لم تكن موجودة
                return BadRequest("Please provide a hover image or ensure the existing hover image is available.");
            }

            // تحديث باقي خصائص المنتج
            existingProduct.Name = productDto.Name;
            existingProduct.oldPrice = productDto.oldPrice;
            existingProduct.NewPrice = productDto.NewPrice;
            existingProduct.discount = productDto.discount;
            existingProduct.soldOut = productDto.soldOut;
            existingProduct.popular = productDto.popular;
            existingProduct.categoryId = productDto.categoryId;
            existingProduct.productFeaturesId = productDto.productFeaturesId;

            // حفظ التعديلات في قاعدة البيانات
            await _productServices.UpdateProduct(id, existingProduct);

            return Ok(existingProduct);
        }







        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] productForAdditionDto productDto)
        {
            // التحقق من وجود الصورة الرئيسية
            if (productDto.ImageUrl == null || productDto.ImageUrl.Length == 0)
            {
                return BadRequest("Please upload a valid main image.");
            }

            // التحقق من وجود صورة hover
            if (productDto.ImageUrlHover == null || productDto.ImageUrlHover.Length == 0)
            {
                return BadRequest("Please upload a valid hover image.");
            }

            // التأكد من وجود المجلد wwwroot/Images
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/products/");

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            // معالجة الصورة الرئيسية
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.ImageUrl.FileName);
            var fullPath = Path.Combine(imagePath, uniqueFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await productDto.ImageUrl.CopyToAsync(stream);
            }

            // معالجة الصورة المتداخلة (Hover)
            var uniqueHoverFileName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.ImageUrlHover.FileName);
            var hoverFullPath = Path.Combine(imagePath, uniqueHoverFileName);

            using (var stream = new FileStream(hoverFullPath, FileMode.Create))
            {
                await productDto.ImageUrlHover.CopyToAsync(stream);
            }

            // حفظ مسار الصورة النسبي في قاعدة البيانات
            var product = new AddProductDto
            {
                Name = productDto.Name,
                ImageUrl = "Images/products/" + uniqueFileName,          // مسار نسبي للصورة الرئيسية
                ImageUrlHover = "Images/products/" + uniqueHoverFileName, // مسار نسبي لصورة الـ hover
                oldPrice = productDto.oldPrice,
                NewPrice = productDto.NewPrice,
                discount = productDto.discount,
                soldOut = productDto.soldOut,
                popular = productDto.popular,
                categoryId = productDto.categoryId,
                productFeaturesId = productDto.productFeaturesId,
                // قم بإضافة خصائص أخرى حسب الحاجة
            };

            // إضافة المنتج إلى قاعدة البيانات
            await _productServices.AddProduct(product);
            return Ok(product);
        }


    }
}
