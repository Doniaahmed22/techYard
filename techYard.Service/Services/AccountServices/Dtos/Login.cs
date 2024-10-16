using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Service.Services.AccountServices.Dtos
{
    public class Login
    {
        [Required(ErrorMessage = " يجب ادخال رقم الهاتف او الايميل")]
        public string PhoneNumberOrEmail { get; set; }

        [Required(ErrorMessage = "يجب ادخال كلمه المرور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;

    }
}
