using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Service.Services.AccountServices.Dtos
{
    public class RegisterCustomer
    {
        [DisplayName("Full Name")]
        [Required(ErrorMessage = "You should enter the Full Name"), StringLength(100)]
        public string FullName { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You should enter the Email")]
        public string Email { get; set; }

        [DisplayName("PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You should enter the Email")]
        public string PhoneNumber { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "You should enter the Password")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "You should confirm the Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match")]
        public string ConfirmPassword { get; set; }

        IFormFile formFile { get; set; }

    }
}
