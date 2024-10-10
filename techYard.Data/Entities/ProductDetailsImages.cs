using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Data.Entities
{
    public class ProductDetailsImages:BaseEntity
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }

        public Products products { get; set; }

    }
}
