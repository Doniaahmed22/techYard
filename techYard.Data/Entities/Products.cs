using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Data.Entities
{
    public class Products : BaseEntity
    {
        public string? Name { get; set; }
        public string? imageUrl { get; set; }
        public string? imageUrlInHover { get; set; }
        public double? oldPrice { get; set; }
        public int? discount { get; set; }
        public bool? soldOut { get; set; }
        public bool? popular { get; set; }
        public string? model { get; set; }
        public string? OS { get; set; }
        public int? categoryId { get; set; }
        public Categories? category { get; set; }
        public ICollection<ProductDetailsImages>? productDetailsImages { get; set; }
        public ICollection<ProductFeatures>? ProductFeatures { get; set; }
        public double? NewPrice
        {
            get
            {
                if (oldPrice.HasValue && discount.HasValue)
                {
                    // Calculate the new price based on the discount percentage
                    return oldPrice - (oldPrice * (discount.Value / 100));
                }
                return null; // Or you could return oldPrice if discount is null
            }
        }

    }
}
