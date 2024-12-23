using Backend.Domain.Entities.Order;
using Backend.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.OrderDetail.Models
{
    public class CreateOrderDetailModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
