using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Data.Entities
{
    public class ProductsInCart : BaseEntity
    {

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Products? Product { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        public int Quantity { get; set; } 

    }
}
