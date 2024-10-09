using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.Service.Services.profileServices
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, getProduct>();
            CreateMap<getProduct, Products>();

            CreateMap<Categories, categoryDto>()
                .ForMember(dest=> dest.imageUrl , options => options.MapFrom<ProductUrlResolver>());

            CreateMap<categoryDto, Categories>();
            CreateMap<AddCategoryDto, Categories>();
            CreateMap<Categories, AddCategoryDto>();


        }
    }
}
