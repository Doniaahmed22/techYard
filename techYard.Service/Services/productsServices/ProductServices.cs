using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Repository.Interfaces;
using techYard.Service.Services.CategoryServices;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.featuresServices.Dtos;
using techYard.Service.Services.FileHandlingService;
using techYard.Service.Services.productsServices.Dtos;
using techYard.Service.Services.profileServices;

namespace techYard.Service.Services.productsServices
{
    public class ProductService : IProductServices
    {
        private readonly IFileHandling _fileHandling;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryServices _categoryServices;
        private readonly IMemoryCache _cache;

        private const string ProductsCacheKey = nameof(CacheMemory.Products);

        public ProductService(IFileHandling fileHandling, IMapper mapper, IUnitOfWork unitOfWork, ICategoryServices categoryServices, IMemoryCache cache)
        {
            _fileHandling = fileHandling;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryServices = categoryServices;
            _cache = cache;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            if (!_cache.TryGetValue(ProductsCacheKey, out IEnumerable<ProductDto> cachedProducts))
            {
                var products = await _unitOfWork.Repository<Products>().GetAllAsync();
                cachedProducts = _mapper.Map<IEnumerable<ProductDto>>(products);

                foreach (var product in cachedProducts)
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

                // Set cache options
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Adjust as needed

                // Cache the product list
                _cache.Set(ProductsCacheKey, cachedProducts, cacheOptions);
            }

            return cachedProducts;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var products = await GetAllProductsAsync(); // Retrieve from cache if available
            return products.FirstOrDefault(p => p.Id == id);
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Products>(productDto);

            if (productDto.Image != null)
                product.imageUrl = await _fileHandling.SaveFileAsync(productDto.Image);

            if (productDto.ImageInHover != null)
                product.imageUrlInHover = await _fileHandling.SaveFileAsync(productDto.ImageInHover);

            foreach (var image in productDto.ProductDetailsImage)
            {
                var imageurl = await _fileHandling.SaveFileAsync(image);
                ProductDetailsImages productDetailsImages = new ProductDetailsImages()
                {
                    ImageUrl = imageurl,
                    Product = product,
                    ProductId = product.Id
                };
                product.productDetailsImages.Add(productDetailsImages);
                await _unitOfWork.Repository<ProductDetailsImages>().AddAsync(productDetailsImages);
            }
            await _unitOfWork.Repository<Products>().AddAsync(product);
            await _unitOfWork.CompleteAsync();

            // Clear cache to ensure updated data is fetched on next retrieval
            _cache.Remove(ProductsCacheKey);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _unitOfWork.Repository<Products>().GetByIdAsync(id);
            if (product == null)
                return null;

            _mapper.Map(productDto, product);

            if (productDto.Image != null)
                product.imageUrl = await _fileHandling.SaveFileAsync(productDto.Image);

            if (productDto.ImageInHover != null)
                product.imageUrlInHover = await _fileHandling.SaveFileAsync(productDto.ImageInHover);

            product.productDetailsImages.Clear();
            foreach (var feature in _mapper.Map<List<ProductDetailsImages>>(productDto.ProductDetailsImage))
            {
                product.productDetailsImages.Add(feature);
            }

            await _unitOfWork.Repository<Products>().Update(product);
            await _unitOfWork.CompleteAsync();

            // Clear cache to ensure updated data is fetched on next retrieval
            _cache.Remove(ProductsCacheKey);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Repository<Products>().GetByIdAsync(id);
            if (product == null)
                return false;

            _unitOfWork.Repository<Products>().Delete(id);
            await _unitOfWork.CompleteAsync();

            // Clear cache to ensure updated data is fetched on next retrieval
            _cache.Remove(ProductsCacheKey);

            return true;
        }

        public async Task<IEnumerable<ProductDto>> GetLaptopsAsync()
        {
            var cacheKey = ProductsCacheKey + "_Laptops";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<ProductDto> cachedLaptops))
            {
                var laptopCategories = await _categoryServices.GetAllCategoriesAsync();
                var laptopCategoryIds = laptopCategories
                    .Where(c => c.Name.Equals("MacBook", StringComparison.OrdinalIgnoreCase))
                    .Select(c => c.Id)
                    .ToList();

                var laptops = await GetAllProductsAsync();
                cachedLaptops = laptops.Where(p => laptopCategoryIds.Contains(p.categoriesId)).ToList();

                // Set cache options
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Adjust as needed

                // Cache the laptop products
                _cache.Set(cacheKey, cachedLaptops, cacheOptions);
            }

            return cachedLaptops;
        }

        public async Task<IEnumerable<ProductDto>> GetDesktopsAsync()
        {
            var cacheKey = ProductsCacheKey + "_Desktops";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<ProductDto> cachedDesktops))
            {
                var desktopCategories = await _categoryServices.GetAllCategoriesAsync();
                var desktopCategoryIds = desktopCategories
                    .Where(c => new List<string> { "iMac", "Mac Pro", "Mac Studio", "Mac Mini" }
                    .Contains(c.Name, StringComparer.OrdinalIgnoreCase))
                    .Select(c => c.Id)
                    .ToList();

                var desktops = await GetAllProductsAsync();
                cachedDesktops = desktops.Where(p => desktopCategoryIds.Contains(p.categoriesId)).ToList();

                // Set cache options
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Adjust as needed

                // Cache the desktop products
                _cache.Set(cacheKey, cachedDesktops, cacheOptions);
            }

            return cachedDesktops;
        }

        public async Task<IEnumerable<ProductDto>> GetAccessoriesAsync()
        {
            var cacheKey = ProductsCacheKey + "_Accessories";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<ProductDto> cachedAccessories))
            {
                var accessoryCategories = await _categoryServices.GetAllCategoriesAsync();
                var accessoryCategoryIds = accessoryCategories
                    .Where(c => c.Name.Equals("Accessories", StringComparison.OrdinalIgnoreCase))
                    .Select(c => c.Id)
                    .ToList();

                var accessories = await GetAllProductsAsync();
                cachedAccessories = accessories.Where(p => accessoryCategoryIds.Contains(p.categoriesId)).ToList();

                // Set cache options
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Adjust as needed

                // Cache the accessory products
                _cache.Set(cacheKey, cachedAccessories, cacheOptions);
            }

            return cachedAccessories;
        }
    }
}
