using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Service.Services.AccountServices.Dtos
{
    public class ProfileImage
    {

        public IFormFile NewImage { get; set; }
    }
}
