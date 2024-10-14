using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Data.Entities
{
    public class ProductDetailsImages:BaseEntity
    {
        public string? ImageUrl { get; set; }

        //public int ProductId {  get; set; }
        //[ForeignKey(nameof(ProductId))]
        //public Products product { get; set; }

    }
}
