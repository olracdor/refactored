using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Product.EntityConfigurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Entities.Product>
    {
        public ProductEntityConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<Entities.Product> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            
        }
    }
}
