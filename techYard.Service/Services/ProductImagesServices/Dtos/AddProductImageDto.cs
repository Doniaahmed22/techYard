using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Service.Services.ProductImagesServices.Dtos
{
    public class AddProductImageDto
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductId { get; set; }
    }
}
