using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Vegist.Models;

namespace Vegist.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
