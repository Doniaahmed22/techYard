using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.featuresServices.Dtos;
using techYard.Service.Services.ProductImagesServices.Dtos;

namespace techYard.Service.Services.productsServices.Dtos
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? ImageInHover { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageUrlInHover { get; set; }
        public double OldPrice { get; set; }
        public int Discount { get; set; }
        public bool SoldOut { get; set; }
        public bool Popular { get; set; }
        public string Model { get; set; }
        public string OS { get; set; }
        public int categoriesId { get; set; }
        public categoryDto? categoryDto { get; set; }= new categoryDto();
        public List<GetFeatureDto>? ProductFeature { get; set; } 
        public List<string>? ProductDetailsImages { get; set; } 
        public List<IFormFile>? ProductDetailsImage { get; set; }
    }
}