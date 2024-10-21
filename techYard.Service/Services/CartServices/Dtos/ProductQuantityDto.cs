using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Service.Services.CartServices.Dtos
{
    public class ProductQuantityDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double? oldPrice { get; set; }
        public double? newPrice { get; set; }
        public int? discount { get; set; }
        public int Quantity { get; set; }
    }
}
