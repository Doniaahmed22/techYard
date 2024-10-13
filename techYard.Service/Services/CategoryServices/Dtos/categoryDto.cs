using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Service.Services.CategoryServices.Dtos
{
    public class categoryDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string? imageUrl { get; set; }
        public ICollection<Products>? products { get; set; }
    }
}
