using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.Service.Services.CategoryServices
{
    public interface ICategoryServices
    {
        Task<IEnumerable<categoryDto>> GetAllCategoriesAsync();
        Task<categoryDto> GetCategoryByIdAsync(int id);
        Task<categoryDto> CreateCategoryAsync(categoryDto categoryDto);
        Task<categoryDto> UpdateCategoryAsync(int id, categoryDto categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(int categoryId);

    }
}
