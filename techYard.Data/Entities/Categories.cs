﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Data.Entities
{
    public class Categories :BaseEntity {
        public string name { get; set; }
        public string? imageUrl { get; set; }
        public ICollection<Products>? products { get; set; } =new List<Products>();

    }
}
