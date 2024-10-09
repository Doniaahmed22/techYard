using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using techYard.Service.Services.productsServices.Dtos;
using techYard.Service.Services.productsServices;
using techYard.Service.Services.CategoryServices;
using techYard.Service.Services.CategoryServices.Dtos;
using Microsoft.AspNetCore.Identity;
using techYard.Data.Entities;

namespace techYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryServices _categoryServices;
        public CategoriesController(ICategoryServices categoryServices)
        {
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
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var category = await _categoryServices.GetCategoryById(id);
            return Ok(category);
        }




        //[HttpPost]
        //[Route("AddCategory")]
        //public async Task<IActionResult> AddCategory(AddCategoryDto categoryDto)
        //{
        //    if (categoryDto == null)
        //    {
        //        return BadRequest("Category is Empty");
        //    }

        //    if (categoryDto.name == null)
        //    {
        //        return BadRequest("name mustn't be empty");
        //    }

        //    await _categoryServices.AddCategory(categoryDto);

        //    return Ok(categoryDto);
        //}





        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory([FromForm] string name, [FromForm] IFormFile imageFile)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name mustn't be empty");
            }

            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Image file is required");
            }

            // حدد مسار لحفظ الصورة
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/category", imageFile.FileName);

            // حفظ الصورة في المسار المحدد
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // هنا يمكنك حفظ بيانات الفئة (الاسم ورابط الصورة) في قاعدة البيانات
            var category = new AddCategoryDto
            {
                name = name,
                imageUrl = "/Images/category" + imageFile.FileName // يمكنك تعديل الرابط حسب الحاجة
            };

            await _categoryServices.AddCategory(category);

            return Ok(new { Name = name, ImageUrl = category.imageUrl });
        }






    }
}
