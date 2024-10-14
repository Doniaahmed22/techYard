using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Service.Services.productsServices.Dtos
{
    public class getProduct
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? imageUrl { get; set; }
        public string? imageUrlInHover { get; set; }
        public double? oldPrice { get; set; }
        public int? discount { get; set; }
        public bool? soldOut { get; set; }
        public bool? popular { get; set; }
        public string? model { get; set; }
        public string? OS { get; set; }
        public int? categoriesId { get; set; }
        //public Categories? category { get; set; }
        public ICollection<ProductDetailsImages>? productDetailsImages { get; set; }
        public ICollection<ProductFeatures>? ProductFeatures { get; set; }
        public double? NewPrice
        {
            get
            {
                if (oldPrice.HasValue && discount.HasValue)
                {
                    double discountValue = (double) (oldPrice * (double)((double)discount / 100));
                    // Calculate the new price based on the discount percentage
                    return oldPrice - discountValue;
                }
                return oldPrice; // Or you could return oldPrice if discount is null
            }
        }
    }
}
