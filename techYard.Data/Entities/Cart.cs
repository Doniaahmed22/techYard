using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Data.Entities
{
    public class Cart: BaseEntity
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<ProductsInCart> ProductsInCart { get; set; } = new List<ProductsInCart>();
    }
}
