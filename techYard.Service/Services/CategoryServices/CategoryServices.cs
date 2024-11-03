using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using techYard.Data.Context;
using techYard.Data.Entities;
using techYard.Repository.Interfaces;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.featuresServices.Dtos;
using techYard.Service.Services.FileHandlingService;
using techYard.Service.Services.productsServices.Dtos;
using techYard.Service.Services.profileServices;

namespace techYard.Service.Services.CategoryServices
{
    public class CategoryService : ICategoryServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileHandling _fileHandling;
        private readonly IMemoryCache _cache;

        private const string CategoriesCacheKey = nameof(CacheMemory.Categories);
        private const string ProductsByCategoryCacheKey = nameof(CacheMemory.Products) + "_";

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork, IFileHandling fileHandling, IMemoryCache cache)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileHandling = fileHandling;
            _cache = cache;
        }

        public async Task<IEnumerable<categoryDto>> GetAllCategoriesAsync()
        {
            if (!_cache.TryGetValue(CategoriesCacheKey, out IEnumerable<categoryDto> cachedCategories))
            {
                var categories = await _unitOfWork.Repository<Categories>().GetAllAsync();
                foreach (var category in categories)
                {
                    category.imageUrl = _fileHandling.GetFileUrl(category.imageUrl);
                }

                cachedCategories = _mapper.Map<IEnumerable<categoryDto>>(categories);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Adjust as needed

                _cache.Set(CategoriesCacheKey, cachedCategories, cacheOptions);
            }

            return cachedCategories;
        }

        public async Task<categoryDto> GetCategoryByIdAsync(int id)
        {
            var categories = await GetAllCategoriesAsync(); // Retrieve from cache if available
            return categories.FirstOrDefault(c => c.Id == id);
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

            // Clear cache to ensure updated data is fetched on next retrieval
            _cache.Remove(CategoriesCacheKey);

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

            // Clear cache to ensure updated data is fetched on next retrieval
            _cache.Remove(CategoriesCacheKey);

            return _mapper.Map<categoryDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Repository<Categories>().GetByIdAsync(id);

            if (category == null)
                return false;

            await _unitOfWork.Repository<Categories>().Delete(id);
            await _unitOfWork.CompleteAsync();

            // Clear cache to ensure updated data is fetched on next retrieval
            _cache.Remove(CategoriesCacheKey);

            return true;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var cacheKey = ProductsByCategoryCacheKey + categoryId;

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<ProductDto> cachedProducts))
            {
                var products = await _unitOfWork.Repository<Products>().GetAllAsync();
                var filteredProducts = products.Where(p => p.categoriesId == categoryId);

                var productDTO = _mapper.Map<IEnumerable<ProductDto>>(filteredProducts);
                foreach (var product in productDTO)
                {
                    product.ImageUrl = _fileHandling.GetFileUrl(product.ImageUrl);
                    product.ImageUrlInHover = _fileHandling.GetFileUrl(product.ImageUrlInHover);
                    product.categoryDto = _mapper.Map<categoryDto>(await _unitOfWork.Repository<Categories>().GetByIdAsync(product.categoriesId));
                    var productFeatures = _unitOfWork.Repository<ProductFeatures>().GetAllAsync().Result.Where(a => a.ProductsId == product.Id).ToList();
                    product.ProductFeature = _mapper.Map<List<GetFeatureDto>>(productFeatures);
                    product.ProductDetailsImages = _unitOfWork.Repository<ProductDetailsImages>().GetAllAsync().Result
                        .Where(a => a.ProductId == product.Id)
                        .Select(a => _fileHandling.GetFileUrl(a.ImageUrl))
                        .ToList();
                }

                cachedProducts = productDTO;

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Adjust as needed

                _cache.Set(cacheKey, cachedProducts, cacheOptions);
            }

            return cachedProducts;
        }
    }
}
