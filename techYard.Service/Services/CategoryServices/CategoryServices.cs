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
using techYard.Service.Services.FileHandlingService;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.Service.Services.CategoryServices
{
    public class CategoryService : ICategoryServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileHandling _fileHandling; // Assuming an existing IFileHandling service for image handling

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork, IFileHandling fileHandling)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileHandling = fileHandling;
        }

        public async Task<IEnumerable<categoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Repository<Categories>().GetAllAsync();
            return _mapper.Map<IEnumerable<categoryDto>>(categories);
        }

        public async Task<categoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Repository<Categories>().GetByIdAsync(id);
            return _mapper.Map<categoryDto>(category);
        }

        public async Task<categoryDto> CreateCategoryAsync(categoryDto categoryDto)
        {
            var category = _mapper.Map<Categories>(categoryDto);

            if (categoryDto.Image != null)
            {
                category.imageUrl = await _fileHandling.SaveFileAsync(categoryDto.Image);
            }

            await _unitOfWork.Repository<Categories>().AddAsync(category);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<categoryDto>(category);
        }

        public async Task<categoryDto> UpdateCategoryAsync(int id, categoryDto categoryDto)
        {
            var category = await _unitOfWork.Repository<Categories>().GetByIdAsync(id);

            if (category == null)
                return null;

            _mapper.Map(categoryDto, category);

            if (categoryDto.Image != null)
            {
                category.imageUrl = await _fileHandling.SaveFileAsync(categoryDto.Image);
            }

            await _unitOfWork.Repository<Categories>().Update(category);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<categoryDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Repository<Categories>().GetByIdAsync(id);

            if (category == null)
                return false;

            await _unitOfWork.Repository<Categories>().Delete(id);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
