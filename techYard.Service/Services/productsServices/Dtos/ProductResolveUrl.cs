using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Service.Services.CategoryServices.Dtos;

namespace techYard.Service.Services.productsServices.Dtos
{
    public class ProductResolveUrl: IValueResolver<Products, getProduct, string>
    {
        private readonly IConfiguration _configuration;

        public ProductResolveUrl(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Products source, getProduct destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.imageUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.imageUrl}";
            }
            return null;
        }
    }

    public class ProductHoverImageUrlResolver : IValueResolver<Products, getProduct, string>
    {
        private readonly IConfiguration _configuration;

        public ProductHoverImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Products source, getProduct destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.imageUrlInHover))
            {
                return $"{_configuration["BaseUrl"]}{source.imageUrlInHover}";
            }
            return null;
        }
    }

}

