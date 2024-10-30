using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Repository.Interfaces;
using techYard.Repository.Repositories;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.featuresServices.Dtos;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.Service.Services.productsServices
{
    public class ProductServices : IProductServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Repository<Products>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Repository<Products>().GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Products>(productDto);
            await _unitOfWork.Repository<Products>().AddAsync(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Products>(productDto);
            await _unitOfWork.Repository<Products>().Update(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _unitOfWork.Repository<Products>().Delete(id);
        }
        //    readonly IUnitOfWork _unitOfWork;
        //    readonly IMapper _mapper;
        //    public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        //    {
        //        _mapper = mapper;
        //        _unitOfWork = unitOfWork;
        //    }



        //    //public async Task<IReadOnlyList<getProduct>> GetAllProducts()
        //    //{
        //    //    // جلب المنتجات مع العلاقات المرتبطة (مثال على Category و ProductFeatures)
        //    //    var products = await _unitOfWork.Repository<Products>().GetAllAsync();
        //    //        //    p => p.category,
        //    //        //    p => p.productFeatures
        //    //        //);

        //    //    // تحويل الكيانات إلى Dto باستخدام AutoMapper
        //    //    return _mapper.Map<IReadOnlyList<getProduct>>(products);
        //    //}

        //    public async Task<IReadOnlyList<getProduct>> GetAllProducts()
        //    {
        //        // جلب المنتجات مع العلاقات المرتبطة (مثال على Category و ProductFeatures)
        //        var products = await _unitOfWork.Repository<Products>().GetAllAsync();

        //        // تحويل الكيانات إلى Dto باستخدام AutoMapper
        //        return _mapper.Map<IReadOnlyList<getProduct>>(products);
        //    }



        //    //public async Task<IReadOnlyList<getProduct>> GetAllProducts()
        //    //{
        //    //    // جلب المنتجات مع تضمين الفئات والصور المرتبطة
        //    //    var products = await _unitOfWork.Repository<Products>().GetAllAsync(
        //    //        p => p.ProductFeatures,
        //    //        p => p.productDetailsImages
        //    //    );

        //    //    // تحويل الكيانات إلى DTO باستخدام AutoMapper
        //    //    var result = _mapper.Map<IReadOnlyList<getProduct>>(products);

        //    //    return result;
        //    //}




        //    //public async Task<getProduct> GetProductById(int id)
        //    //{
        //    //    var products = await _unitOfWork.Repository<Products>().GetByIdAsync(id);

        //    //    return _mapper.Map<getProduct>(products);
        //    //}


        //    public async Task<getProduct?> GetProductById(int id)
        //    {
        //        var product = await _unitOfWork.Repository<Products>().GetByIdAsync(
        //            id,
        //            p => p.productDetailsImages,
        //            p => p.ProductFeatures
        //        );

        //        var result = _mapper.Map<getProduct>(product);

        //        return result;
        //    }



        //    public async Task<getProduct> UpdateProduct(int id, getProduct productDto)
        //    {
        //        var product = await _unitOfWork.Repository<Products>().GetByIdAsync(id);
        //        if (product == null)
        //        {
        //            return null;
        //        }
        //        _mapper.Map(productDto, product);
        //        await _unitOfWork.Repository<Products>().Update(product);
        //        await _unitOfWork.CompleteAsync();
        //        return productDto;
        //    }



        //    public async Task<AddProductDto> AddProduct(AddProductDto productDto)
        //    {
        //        var Product = _mapper.Map<Products>(productDto);
        //        await _unitOfWork.Repository<Products>().AddAsync(Product);
        //        await _unitOfWork.CompleteAsync();
        //        return productDto;
        //    }

        //    public async Task<Products?> DeleteProduct(int id)
        //    {
        //        var product = await _unitOfWork.Repository<Products>().GetByIdAsync(id);
        //        if (product == null)
        //        {
        //            return null;
        //        }
        //        await _unitOfWork.Repository<Products>().Delete(id);
        //        await _unitOfWork.CompleteAsync();  // Save the changes
        //        return product;
        //    }
    }

}

