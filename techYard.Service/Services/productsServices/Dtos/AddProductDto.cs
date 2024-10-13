using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Service.Services.productsServices.Dtos
{
    public class AddProductDto
    {
        public string? Name { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlHover { get; set; }
        public double? oldPrice { get; set; }
        public double? NewPrice { get; set; }
        public int? discount { get; set; }
        public bool? soldOut { get; set; }
        public bool? popular { get; set; }
        public int? categoryId { get; set; }
        public int? productFeaturesId { get; set; }
    }
}
