using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Service.Services.CategoryServices.Dtos
{
    public class ProductUrlResolver : IValueResolver<Categories, categoryDto, string>
    {
        readonly IConfiguration _configuration;
        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string Resolve(Categories source, categoryDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.imageUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.imageUrl}";
            }
            return null;
        }
    }
}
