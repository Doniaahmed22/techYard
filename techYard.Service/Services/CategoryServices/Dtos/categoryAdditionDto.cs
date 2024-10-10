using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace techYard.Service.Services.CategoryServices.Dtos
{
    public class categoryAdditionDto
    {
        public string Name { get; set; }
        public IFormFile imageUrl { get; set; } // استقبال الصورة كملف
    }
}
