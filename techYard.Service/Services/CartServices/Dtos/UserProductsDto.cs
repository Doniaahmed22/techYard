using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Service.Services.CartServices.Dtos
{
    public class UserProductsDto
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public List<ProductQuantityDto> ProductByUser { get; set; }
    }
}
