using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Data.Context
{
    public class techYardDbContext : DbContext
    {
        public techYardDbContext(DbContextOptions<techYardDbContext> options) : base(options)
        {

        }

        public DbSet<Products> products {  get; set; } 
        public DbSet<Categories> categories {  get; set; }
        public DbSet<ProductDetailsImages> productDetailsImages {  get; set; }
        public DbSet<ProductFeatures> productFeatures {  get; set; }

    }
}
