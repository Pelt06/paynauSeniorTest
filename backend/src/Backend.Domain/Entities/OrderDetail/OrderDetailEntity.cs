using Backend.Domain.Entities.Order;
using Backend.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.OrderDetail
{
    public class OrderDetailEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderEntity? Order { get; set; }
        public int ProductId { get; set; }
        public ProductEntity? Product { get; set; }
        public int Quantity { get; set; }
    }
}
