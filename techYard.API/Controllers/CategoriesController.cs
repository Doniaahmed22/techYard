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


        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryServices.DeleteCategoryById(id);
            if(category == null)
            {
                return NotFound("Invalid Id");
            }
            return Ok();
        }




        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(int id, AddCategoryDto categorDto)
        {
            if (categorDto == null)
            {
                return BadRequest("category is Empty");
            }

            var category = await _categoryServices.UpdateCategory(id, categorDto);
            if (category == null)
            {
                return NotFound("Invalid Id");
            }
            return Ok();
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
