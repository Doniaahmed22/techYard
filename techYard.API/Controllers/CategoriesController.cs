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
using Microsoft.AspNetCore.Authorization;

namespace techYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryServices _categoryService;
        readonly IUnitOfWork _unitOfWork;
        public CategoriesController(ICategoryServices categoryServices, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryServices;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<ActionResult<IEnumerable<categoryDto>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("GetCategoryById/{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpDelete("DeleteCategory/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost]
        [Route("UpdateCategory/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] categoryDto categoryDto)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            if (updatedCategory == null) return NotFound();
            return Ok(updatedCategory);
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory([FromForm] categoryDto categoryDto)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
            return Ok(createdCategory);
        }
    }
}
