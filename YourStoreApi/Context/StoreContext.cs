using Microsoft.EntityFrameworkCore;
using System.Reflection;
using YourStoreApi.Models;
using YourStoreApi.Models.OderAggregate;

namespace YourStoreApi.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Colour> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        // public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
