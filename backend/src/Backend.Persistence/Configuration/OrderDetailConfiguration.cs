using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Entities.OrderDetail;

namespace Backend.Persistence.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetailEntity>
    {
        public void Configure(EntityTypeBuilder<OrderDetailEntity> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.HasOne(x => x.Order)
                .WithMany(x => x.OrderDetail)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.HasOne(x => x.Product)
                .WithMany(x => x.OrderDetail)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.Property(x => x.Quantity).IsRequired();
        }
    }
}
