using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Repository.Interfaces;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.Service.Services.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        public CategoryServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<categoryDto>> GetAllCategories()
        {
            var categories = await _unitOfWork.Repository<Categories>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<categoryDto>>(categories);
        }

        public async Task<categoryDto> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Repository<Categories>().GetByIdAsync(id);
            return _mapper.Map<categoryDto>(category);
        }

        public async Task AddCategory(AddCategoryDto categoryDto)
        {
            var Category = _mapper.Map<Categories>(categoryDto);
            await _unitOfWork.Repository<Categories>().AddAsync(Category);
        }
    }
}
