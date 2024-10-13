using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Service.Services.ProductImagesServices.Dtos
{
    public class GetProductDetailsImagesDto
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }

        public int ProductId { get; set; }

        public Products products { get; set; }
    }
}
