using Backend.Domain.Entities.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Order
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; } = 0;
        public ICollection<OrderDetailEntity>? OrderDetail { get; set; }
    }
}
