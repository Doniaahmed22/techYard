using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Context;
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
        readonly techYardDbContext _context;
        public CategoryServices(IUnitOfWork unitOfWork, IMapper mapper, techYardDbContext context)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IReadOnlyList<categoryDto>> GetAllCategories()
        {
            var categories = await _unitOfWork.Repository<Categories>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<categoryDto>>(categories);
        }

        public async Task<categoryDto> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Repository<Categories>().GetByIdAsync(id);
            if(category == null)
            {
                return null;
            }
            return _mapper.Map<categoryDto>(category);
        }

        public async Task AddCategory(AddCategoryDto categoryDto)
        {
            var Category = _mapper.Map<Categories>(categoryDto);
            await _unitOfWork.Repository<Categories>().AddAsync(Category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<categoryDto> DeleteCategoryById (int id)
        {
            var category = await _unitOfWork.Repository<Categories>().GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }
            await _unitOfWork.Repository<Categories>().Delete(id);
            await _unitOfWork.CompleteAsync();
            return new categoryDto { };
            
        }


        public async Task<categoryDto> UpdateCategory(int id, AddCategoryDto category)
        {
            var existingCategory = await _unitOfWork.Repository<Categories>().GetByIdAsync(id);
            if (existingCategory == null)
            {
                return null;
            }

            _mapper.Map(category, existingCategory);
            await _unitOfWork.Repository<Categories>().Update(existingCategory);
            await _unitOfWork.CompleteAsync();
            return new categoryDto { };

        }



    }
}
