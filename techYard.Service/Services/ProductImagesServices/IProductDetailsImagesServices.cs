using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Service.Services.ProductImagesServices
{
    public interface IProductDetailsImagesServices
    {
        Task<List<ProductDetailsImages>> uploadImages(List<IFormFile> images);
    }
}