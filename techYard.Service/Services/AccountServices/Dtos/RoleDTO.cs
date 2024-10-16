using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Service.Services.AccountServices.Dtos
{
    public class RoleDTO
    {
        [Required, Display(Name = "Name"), StringLength(50)]
        public string Name { get; set; }
        [Required, Display(Name = "Description"), StringLength(int.MaxValue)]
        public string Description { get; set; }

    }
}
