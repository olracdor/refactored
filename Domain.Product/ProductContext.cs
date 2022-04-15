
using Domain.Product.Entities;
using Domain.Product.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Product
{
    public class ProductContext : DbContext
    {

        public DbSet<Entities.Product> Product { get; set; }
        public DbSet<ProductOption> ProductOption { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductOptionEntityConfiguration());

        }
        public DbContext DbContext
        {
            get { return this; }
        }

    }
}