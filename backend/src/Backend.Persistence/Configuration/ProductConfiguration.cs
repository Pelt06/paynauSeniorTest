using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Entities.Product;

namespace Backend.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.Description).IsRequired().HasMaxLength(250);
            entityBuilder.Property(x => x.Price).IsRequired().HasPrecision(18, 2);
            entityBuilder.Property(x => x.Stock).IsRequired();
            entityBuilder.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
