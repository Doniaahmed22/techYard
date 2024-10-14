using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.featuresServices.Dtos;
using techYard.Service.Services.ProductImagesServices.Dtos;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.Service.Services.profileServices
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // خريطة تحويل من Products إلى getProduct
            CreateMap<Products, getProduct>();
            CreateMap<getProduct, Products>();

            // خريطة تحويل من AddProductDto إلى Products
            CreateMap<AddProductDto, Products>()
                .ForMember(dest => dest.imageUrlInHover, options => options.MapFrom(src => src.imageUrlInHover));

            // خريطة تحويل من Products إلى AddProductDto
            CreateMap<Products, AddProductDto>()
                .ForMember(dest => dest.imageUrlInHover, options => options.MapFrom(src => src.imageUrlInHover));

            // خريطة تحويل من Categories إلى categoryDto مع استخدام المحلل ProductUrlResolver
            CreateMap<Categories, categoryDto>()
                .ForMember(dest => dest.imageUrl, options => options.MapFrom<ProductUrlResolver>());

            // خريطة تحويل من categoryDto إلى Categories
            CreateMap<categoryDto, Categories>();
            CreateMap<AddCategoryDto, Categories>();
            CreateMap<Categories, AddCategoryDto>();

            // خريطة تحويل من AddFeatureDto إلى ProductFeatures
            CreateMap<AddFeatureDto, ProductFeatures>();
            CreateMap<ProductFeatures, AddFeatureDto>();

            // خريطة تحويل من GetFeatureDto إلى ProductFeatures
            CreateMap<GetFeatureDto, ProductFeatures>();
            CreateMap<ProductFeatures, GetFeatureDto>();

            // خريطة تحويل من Products إلى ProductDto مع استخدام المحلل ProductImageUrlResolver
            CreateMap<Products, getProduct>()
                .ForMember(dest => dest.imageUrl, opt => opt.MapFrom<ProductResolveUrl>())
                .ForMember(dest => dest.imageUrlInHover, opt => opt.MapFrom<ProductHoverImageUrlResolver>());


            CreateMap<ProductDetailsImages, GetProductDetailsImagesDto>();
            CreateMap<GetProductDetailsImagesDto, ProductDetailsImages>();








            CreateMap<ProductDetailsImages, GetProductDetailsImagesDto>()
                .ReverseMap();
            //// خريطة تحويل من Products إلى getProduct مع تجاهل العلاقات الدورية
            //CreateMap<Products, getProduct>()
            //    .ForMember(dest => dest.category, opt => opt.Ignore())  // تجاهل الفئة لتجنب الدوران
            //    .ForMember(dest => dest.productFeatures, opt => opt.Ignore());  // تجاهل الميزات

            //// خريطة تحويل من ProductFeatures إلى GetFeatureDto مع تجاهل المنتجات
            //CreateMap<ProductFeatures, GetFeatureDto>()
            //    .ForMember(dest => dest.products, opt => opt.Ignore());

            //// خريطة تحويل من Categories إلى categoryDto مع تجاهل المنتجات
            //CreateMap<Categories, categoryDto>();
            //CreateMap<Categories, categoryDto>()
            //    .ForMember(dest => dest.products, opt => opt.Ignore());






            //CreateMap<Categories, categoryDto>()
            //.ForMember(dest => dest.products, opt => opt.MapFrom(src => src.products));

            //CreateMap<Products, getProduct>()
            //    .ForMember(dest => dest.productFeatures, opt => opt.Ignore())
            //    .ForMember(dest => dest.productDetailsImages, opt => opt.Ignore());

        }
    }
}
