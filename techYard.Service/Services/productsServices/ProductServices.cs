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
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



        //public async Task<IReadOnlyList<getProduct>> GetAllProducts()
        //{
        //    // جلب المنتجات مع العلاقات المرتبطة (مثال على Category و ProductFeatures)
        //    var products = await _unitOfWork.Repository<Products>().GetAllAsync();
        //        //    p => p.category,
        //        //    p => p.productFeatures
        //        //);

        //    // تحويل الكيانات إلى Dto باستخدام AutoMapper
        //    return _mapper.Map<IReadOnlyList<getProduct>>(products);
        //}

        //public async Task<IReadOnlyList<getProduct>> GetAllProducts()
        //{
        //    // جلب المنتجات مع العلاقات المرتبطة (مثال على Category و ProductFeatures)
        //    var products = await _unitOfWork.Repository<Products>().GetAllAsync();

        //    // تحويل الكيانات إلى Dto باستخدام AutoMapper
        //    return _mapper.Map<IReadOnlyList<getProduct>>(products);
        //}



        public async Task<IReadOnlyList<getProduct>> GetAllProducts()
        {
            var products = await _unitOfWork.Repository<Products>().GetAllAsync(
                p => p.category,
                p => p.productDetailsImages
            );

            _mapper.Map<Products>(products);

            return products;
        }




        public async Task<getProduct> GetProductById(int id)
        {
            var products = await _unitOfWork.Repository<Products>().GetByIdAsync(id);

            return _mapper.Map<getProduct>(products);
        }


        //public async Task<getProduct?> GetProductById(int id)
        //{
        //    var product = await _unitOfWork.Repository<Products>().GetAllAsync(
        //        p => p.category,
        //        p => p.productFeatures
        //    );

        //    var result = product
        //        .Where(p => p.Id == id)
        //        .Select(p => new getProduct
        //        {
        //            Id = p.Id,
        //            Name = p.Name,
        //            imageUrl = p.imageUrl,
        //            imageUrlInHover = p.imageUrlInHover,
        //            oldPrice = p.oldPrice,
        //            NewPrice = p.NewPrice,
        //            discount = p.discount,
        //            soldOut = p.soldOut,
        //            popular = p.popular,
        //            category = new Categories { Id = p.category.Id, name = p.category.name },
        //            productFeatures = new ProductFeatures { Id = p.productFeatures.Id, model = p.productFeatures.model }
        //        })
        //        .FirstOrDefault();

        //    return result;
        //}



        public async Task<getProduct> UpdateProduct(int id, getProduct productDto)
        {
            var product = await _unitOfWork.Repository<Products>().GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            _mapper.Map(productDto, product);
            await _unitOfWork.Repository<Products>().Update(product);
            await _unitOfWork.CompleteAsync();
            return productDto;
        }



        public async Task<AddProductDto> AddProduct(AddProductDto productDto)
        {
            var Product = _mapper.Map<Products>(productDto);
            await _unitOfWork.Repository<Products>().AddAsync(Product);
            await _unitOfWork.CompleteAsync();
            var AddedProduct = _mapper.Map(Product, productDto);
            return AddedProduct;
        }

        public async Task<Products?> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Repository<Products>().GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            await _unitOfWork.Repository<Products>().Delete(id);
            await _unitOfWork.CompleteAsync();  // Save the changes
            return product;
        }
    }
}
