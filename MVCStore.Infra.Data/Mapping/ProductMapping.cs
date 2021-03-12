using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCStore.Domain.Entities;

namespace MVCStore.Infra.Data.Mapping {
    public class ProductMapping : IEntityTypeConfiguration<Product> {
        public void Configure(EntityTypeBuilder<Product> builder) {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("varchar(50)");
            
            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("varchar(200)");
            
            builder.Property(p => p.Image)
                .IsRequired()
                .HasColumnName("Image")
                .HasColumnType("varchar(200)");
        }
    }
}