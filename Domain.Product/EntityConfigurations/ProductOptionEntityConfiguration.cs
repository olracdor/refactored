using Domain.Product.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Product.EntityConfigurations
{
    
    public class ProductOptionEntityConfiguration : IEntityTypeConfiguration<ProductOption>
    {
        public ProductOptionEntityConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<ProductOption> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Relationships
            builder.HasOne(t => t.Product)
                .WithMany(t => t.ProductOptions)
                .HasForeignKey(d => d.ProductId);

        }
    }
}
