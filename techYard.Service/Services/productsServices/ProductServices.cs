using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Repository.Interfaces;
using techYard.Repository.Repositories;
using techYard.Service.Services.CategoryServices;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.featuresServices.Dtos;
using techYard.Service.Services.FileHandlingService;
using techYard.Service.Services.ProductImagesServices.Dtos;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.Service.Services.productsServices
{
    public class ProductService : IProductServices
    {
        private readonly IFileHandling _fileHandling;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryServices _categoryServices;

        public ProductService(IFileHandling fileHandling, IMapper mapper, IUnitOfWork unitOfWork, ICategoryServices categoryServices)
        {
            _fileHandling = fileHandling;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryServices = categoryServices;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Products>().GetAllAsync());
            foreach (var product in products)
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
            return products;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = _mapper.Map<ProductDto>(await _unitOfWork.Repository<Products>().GetByIdAsync(id));
            product.ImageUrl = _fileHandling.GetFileUrl(product.ImageUrl);
            product.ImageUrlInHover = _fileHandling.GetFileUrl(product.ImageUrlInHover);
            product.categoryDto = _mapper.Map<categoryDto>(await _unitOfWork.Repository<Categories>().GetByIdAsync(product.categoriesId));
            product.categoryDto.ImageUrl = _fileHandling.GetFileUrl(product.categoryDto.ImageUrl);
            product.ProductFeature = _mapper.Map<List<GetFeatureDto>>(_unitOfWork.Repository<ProductFeatures>().GetAllAsync().Result.Where(a => a.ProductsId == product.Id).ToList());
            product.ProductDetailsImages = _unitOfWork.Repository<ProductDetailsImages>().GetAllAsync().Result
                .Where(a => a.ProductId == product.Id)
                .Select(a => _fileHandling.GetFileUrl(a.ImageUrl))
                .ToList(); 
            return product;
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
               var imageurl= await _fileHandling.SaveFileAsync(image);
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
            foreach(var feature in _mapper.Map<List<ProductDetailsImages>>(productDto.ProductDetailsImage))
            {
                product.productDetailsImages.Add(feature);
            }

            await _unitOfWork.Repository<Products>().Update(product);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Repository<Products>().GetByIdAsync(id);
            if (product == null)
                return false;

            _unitOfWork.Repository<Products>().Delete(id);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDto>> GetLaptopsAsync()
        {
            var laptopCategories = await _categoryServices.GetAllCategoriesAsync();
            var laptopCategoryIds = laptopCategories
                .Where(c => c.Name.Equals("MacBook", StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Id)
                .ToList();

            var laptops = _mapper.Map<IEnumerable<ProductDto>>(
                await _unitOfWork.Repository<Products>().GetAllAsync()
            ).Where(p => laptopCategoryIds.Contains(p.categoriesId));

            foreach (var product in laptops)
            {
                product.ImageUrl = _fileHandling.GetFileUrl(product.ImageUrl);
                product.ImageUrlInHover = _fileHandling.GetFileUrl(product.ImageUrlInHover);
                product.categoryDto = _mapper.Map<categoryDto>(await _unitOfWork.Repository<Categories>().GetByIdAsync(product.categoriesId));
                product.ProductDetailsImages = _unitOfWork.Repository<ProductDetailsImages>().GetAllAsync().Result
                    .Where(a => a.ProductId == product.Id)
                    .Select(a => _fileHandling.GetFileUrl(a.ImageUrl))
                    .ToList();
            }

            return laptops;
        }

        public async Task<IEnumerable<ProductDto>> GetDesktopsAsync()
        {
            var desktopCategories = await _categoryServices.GetAllCategoriesAsync();
            var desktopCategoryIds = desktopCategories
                .Where(c => new List<string> { "iMac", "Mac Pro", "Mac Studio", "Mac Mini" }
                .Contains(c.Name, StringComparer.OrdinalIgnoreCase))
                .Select(c => c.Id)
                .ToList();

            var desktops = _mapper.Map<IEnumerable<ProductDto>>(
                await _unitOfWork.Repository<Products>().GetAllAsync()
            ).Where(p => desktopCategoryIds.Contains(p.categoriesId));

            foreach (var product in desktops)
            {
                product.ImageUrl = _fileHandling.GetFileUrl(product.ImageUrl);
                product.ImageUrlInHover = _fileHandling.GetFileUrl(product.ImageUrlInHover);
                product.categoryDto = _mapper.Map<categoryDto>(await _unitOfWork.Repository<Categories>().GetByIdAsync(product.categoriesId));
                product.ProductDetailsImages = _unitOfWork.Repository<ProductDetailsImages>().GetAllAsync().Result
                    .Where(a => a.ProductId == product.Id)
                    .Select(a => _fileHandling.GetFileUrl(a.ImageUrl))
                    .ToList();
            }

            return desktops;
        }

        public async Task<IEnumerable<ProductDto>> GetAccessoriesAsync()
        {
            var accessoryCategories = await _categoryServices.GetAllCategoriesAsync();
            var accessoryCategoryIds = accessoryCategories
                .Where(c => c.Name.Equals("Accessories", StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Id)
                .ToList();

            var accessories = _mapper.Map<IEnumerable<ProductDto>>(
                await _unitOfWork.Repository<Products>().GetAllAsync()
            ).Where(p => accessoryCategoryIds.Contains(p.categoriesId));

            foreach (var product in accessories)
            {
                product.ImageUrl = _fileHandling.GetFileUrl(product.ImageUrl);
                product.ImageUrlInHover = _fileHandling.GetFileUrl(product.ImageUrlInHover);
                product.categoryDto = _mapper.Map<categoryDto>(await _unitOfWork.Repository<Categories>().GetByIdAsync(product.categoriesId));
                product.ProductDetailsImages = _unitOfWork.Repository<ProductDetailsImages>().GetAllAsync().Result
                    .Where(a => a.ProductId == product.Id)
                    .Select(a => _fileHandling.GetFileUrl(a.ImageUrl))
                    .ToList();
            }

            return accessories;
        }
    }
}

