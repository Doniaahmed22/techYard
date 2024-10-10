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

        Task<IReadOnlyList<categoryDto>> GetAllCategories();
        Task<categoryDto> GetCategoryById(int id);
        Task AddCategory(AddCategoryDto categoryDto);
        Task<categoryDto> DeleteCategoryById(int id);
        Task<categoryDto> UpdateCategory(int id, AddCategoryDto entity);



    }
}
