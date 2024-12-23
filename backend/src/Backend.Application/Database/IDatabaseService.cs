using Backend.Domain.Entities.Order;
using Backend.Domain.Entities.OrderDetail;
using Backend.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database
{
    public interface IDatabaseService
    {

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderDetailEntity> OrderDetails { get; set; }

        DatabaseFacade db { get; }
        Task<bool> SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
