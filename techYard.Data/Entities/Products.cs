using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Data.Entities
{
    public class Products : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? imageUrl { get; set; }
        public string? imageUrlInHover { get; set; }
        public double? oldPrice { get; set; }
        public double? NewPrice { get; set; }
        public int? discount { get; set; }
        public bool? soldOut { get; set; }
        public bool? popular { get; set; }
        public int? categoryId { get; set; }
        public Categories? category { get; set; }
        public int? productFeaturesId { get; set; }
        public ProductFeatures? productFeatures { get; set; }
        public ICollection<ProductDetailsImages>? productDetailsImages { get; set; }

    }
}
