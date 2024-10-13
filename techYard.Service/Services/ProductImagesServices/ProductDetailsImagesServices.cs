using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Repository.Interfaces;
using techYard.Repository.Repositories;
using techYard.Service.Services.productsServices;

namespace techYard.Service.Services.ProductImagesServices
{
    public class ProductDetailsImagesServices : IProductDetailsImagesServices
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductDetailsImagesServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<ProductDetailsImages>> uploadImages(List<IFormFile> images)
        {
            string url;
            List<ProductDetailsImages> listurl = new List<ProductDetailsImages>();
            foreach (var image in images)
            {
                // التأكد من وجود المجلد wwwroot/Images
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/products/");

                // تأكد من إنشاء المجلد إذا لم يكن موجودًا
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                // التعامل مع الصورة الرئيسية
                // معالجة الصورة الرئيسية
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var fullPath = Path.Combine(imagePath, uniqueFileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                url = "Images/products/" + uniqueFileName; // مسار نسبي للصورة الرئيسية
                var imageproduct = new ProductDetailsImages
                {
                    ImageUrl = url,
                };
                await unitOfWork.Repository<ProductDetailsImages>().AddAsync(imageproduct);
                await unitOfWork.CompleteAsync();
                listurl.Add(imageproduct);
            }
            return listurl;
        }
    }
}