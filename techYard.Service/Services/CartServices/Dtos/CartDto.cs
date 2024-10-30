using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Service.Services.ProductInCartServices.DTO;

namespace techYard.Service.Services.CartServices.Dtos
{
    public class CartDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<ProductInCartDto> ProductsInCart { get; set; } = new List<ProductInCartDto>();
    }
}
