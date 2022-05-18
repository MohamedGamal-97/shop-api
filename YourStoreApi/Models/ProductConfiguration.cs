using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YourStoreApi.Models
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
          builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.ProductBrand).WithMany()
                .HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(p => p.Color).WithMany()
                .HasForeignKey(p => p.Color_Id);
                builder.HasOne(p => p.Size).WithMany()
                .HasForeignKey(p => p.Size_Id);
           builder.HasOne(p => p.SubCategory).WithMany()
                .HasForeignKey(p => p.SubCategory_Id);
        //    builder.HasMany(p => p.ProductReviews).WithOne()
        //         .HasForeignKey(p => p.Customer_Id);
        }
    }
}
