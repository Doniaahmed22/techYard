using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Service.Services.AccountServices.Dtos
{
    public class AuthDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileImageId { get; set; }

    }
}
