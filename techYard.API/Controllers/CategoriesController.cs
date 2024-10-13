using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using techYard.Service.Services.productsServices.Dtos;
using techYard.Service.Services.productsServices;
using techYard.Service.Services.CategoryServices;
using techYard.Service.Services.CategoryServices.Dtos;
using Microsoft.AspNetCore.Identity;
using techYard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using techYard.Repository.Interfaces;

namespace techYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryServices _categoryServices;
        readonly IUnitOfWork _unitOfWork;
        public CategoriesController(ICategoryServices categoryServices, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryServices = categoryServices;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<ActionResult<IEnumerable<categoryDto>>> GetAllCategories()
        {
            var categories = await _categoryServices.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet]
        [Route("GetCategoryById/{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var category = await _categoryServices.GetCategoryById(id);
            if(category == null) 
            {
                return NotFound("invalid Id");
            }
            return Ok(category);
        }


        //[HttpDelete("DeleteCategory/{id}")]
        //public async Task<IActionResult> DeleteCategory(int id)
        //{
        //    var category = await _categoryServices.DeleteCategoryById(id);
        //    if(category == null)
        //    {
        //        return NotFound("Invalid Id");
        //    }
        //    return Ok();
        //}



        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= 0)
            {
                return NotFound("Invalid Id");
            }

            var category = await _categoryServices.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("category not found.");
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
                var mainImagePath = CleanPath(category.imageUrl);
                var fullMainImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", mainImagePath);
                if (System.IO.File.Exists(fullMainImagePath))
                {
                    System.IO.File.Delete(fullMainImagePath);
                }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting images: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the product images.");
            }

            await _categoryServices.DeleteCategoryById(id);
            return Ok($"Category with ID {id} deleted successfully.");
        }






        [HttpPost]
        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] categoryAdditionDto categoryDto)
        {
            // تحقق من وجود الفئة أولاً
            var existingCategory = await _categoryServices.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            string uniqueFileName = existingCategory.imageUrl;  // احتفظ بالمسار الحالي للصورة

            // إذا تم رفع صورة جديدة، قم بتحديث الصورة والمسار
            if (categoryDto.imageUrl != null && categoryDto.imageUrl.Length > 0)
            {
                try
                {
                    // إنشاء مجلد الصور إذا لم يكن موجودًا
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/category/");
                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                        Console.WriteLine("Directory created successfully.");
                    }

                    // حذف الصورة القديمة (إذا كانت موجودة)
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCategory.imageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                        Console.WriteLine("Old image deleted.");
                    }

                    // إنشاء اسم فريد للصورة الجديدة
                    uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(categoryDto.imageUrl.FileName);
                    var fullPath = Path.Combine(imagePath, uniqueFileName);

                    // حفظ الصورة الجديدة في المجلد
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await categoryDto.imageUrl.CopyToAsync(stream);
                        Console.WriteLine("Image saved successfully.");
                    }

                    uniqueFileName = "Images/category/" + uniqueFileName; // تحديث المسار النسبي
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return StatusCode(500, "An error occurred while uploading the image.");
                }
            }

            // تحديث بيانات الفئة
            existingCategory.name = categoryDto.Name;
            existingCategory.imageUrl = uniqueFileName;

            // حفظ التعديلات في قاعدة البيانات
            await _categoryServices.UpdateCategory(id , existingCategory);

            return Ok(existingCategory);
        }





        [HttpPost]
        [Route("AddCategory")]

        public async Task<IActionResult> AddCategory([FromForm] categoryAdditionDto categoryDto)
        {
            if (categoryDto.imageUrl == null || categoryDto.imageUrl.Length == 0)
            {
                return BadRequest("Please upload a valid image.");
            }

            // التأكد من وجود المجلد wwwroot/images
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/category/");

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            // إنشاء اسم فريد للصورة (لتجنب تكرار الأسماء)
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(categoryDto.imageUrl.FileName);

            // تحديد المسار الكامل لحفظ الصورة
            var fullPath = Path.Combine(imagePath, uniqueFileName);

            // حفظ الصورة في المجلد wwwroot/images
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await categoryDto.imageUrl.CopyToAsync(stream);
            }

            // حفظ مسار الصورة النسبي في قاعدة البيانات
            var category = new AddCategoryDto
            {
                name = categoryDto.Name,
                imageUrl = "Images/category/" + uniqueFileName // مسار نسبي للصورة
            };

            // إضافة الفئة إلى قاعدة البيانات
            await _categoryServices.AddCategory(category);
            return Ok(category);
        }       



        //[HttpPost]
        //[Route("AddCategory")]
        //public async Task<IActionResult> AddCategory([FromForm] string name, [FromForm] IFormFile imageFile)
        //{
        //    if (string.IsNullOrEmpty(name))
        //    {
        //        return BadRequest("Name mustn't be empty");
        //    }

        //    if (imageFile == null || imageFile.Length == 0)
        //    {
        //        return BadRequest("Image file is required");
        //    }

        //    // حدد مسار لحفظ الصورة
        //    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/category", imageFile.FileName);

        //    // حفظ الصورة في المسار المحدد
        //    using (var stream = new FileStream(imagePath, FileMode.Create))
        //    {
        //        await imageFile.CopyToAsync(stream);
        //    }

        //    // هنا يمكنك حفظ بيانات الفئة (الاسم ورابط الصورة) في قاعدة البيانات
        //    var category = new AddCategoryDto
        //    {
        //        name = name,
        //        imageUrl = "/Images/category" + imageFile.FileName // يمكنك تعديل الرابط حسب الحاجة
        //    };

        //    await _categoryServices.AddCategory(category);

        //    return Ok(new { Name = name, ImageUrl = category.imageUrl });
        //}








    }
}
