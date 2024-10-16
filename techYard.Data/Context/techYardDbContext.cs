using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Data.Context
{
    public class techYardDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public techYardDbContext(DbContextOptions<techYardDbContext> options) : base(options) 
        {

        }

        public DbSet<Products> products {  get; set; } 
        public DbSet<Categories> categories {  get; set; }
        public DbSet<ProductDetailsImages> productDetailsImages {  get; set; }
        public DbSet<ProductFeatures> productFeatures {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "dbo");
            modelBuilder.Entity<ApplicationRole>().ToTable("Role", "dbo");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "dbo");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "dbo");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "dbo");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "dbo");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "dbo");


        }
    }
}
