using Backend.Application.Database.OrderDetail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Order.Models
{
    public class CreateOrderModel
    {
        public List<CreateOrderDetailModel> OrderDetails { get; set; }
    }
}
