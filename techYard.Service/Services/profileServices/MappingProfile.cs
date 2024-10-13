using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.featuresServices.Dtos;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.Service.Services.profileServices
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //CreateMap<Products, getProduct>();
            //CreateMap<getProduct, Products>();

            //CreateMap<AddProductDto, Products>()
            //    .ForMember(dest => dest.imageUrlInHover, options => options.MapFrom(src => src.ImageUrlHover));

            //CreateMap<Products, AddProductDto>()
            //    .ForMember(dest => dest.ImageUrlHover, options => options.MapFrom(src=>src.imageUrlInHover));



            //CreateMap<Categories, categoryDto>()
            //    .ForMember(dest=> dest.imageUrl , options => options.MapFrom<ProductUrlResolver>());

            //CreateMap<categoryDto, Categories>();
            //CreateMap<AddCategoryDto, Categories>();
            //CreateMap<Categories, AddCategoryDto>();


            //CreateMap<AddFeatureDto, ProductFeatures>();
            //CreateMap<ProductFeatures, AddFeatureDto>();

            //CreateMap<GetFeatureDto, ProductFeatures>();
            //CreateMap<ProductFeatures, GetFeatureDto>();





            // خريطة تحويل من Products إلى getProduct
            CreateMap<Products, getProduct>();
            CreateMap<getProduct, Products>();

            // خريطة تحويل من AddProductDto إلى Products
            CreateMap<AddProductDto, Products>()
                .ForMember(dest => dest.imageUrlInHover, options => options.MapFrom(src => src.ImageUrlHover));

            // خريطة تحويل من Products إلى AddProductDto
            CreateMap<Products, AddProductDto>()
                .ForMember(dest => dest.ImageUrlHover, options => options.MapFrom(src => src.imageUrlInHover));

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






        }
    }
}
