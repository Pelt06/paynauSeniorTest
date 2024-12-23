using Backend.Domain.Entities.Order;
using Backend.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Backend.Domain.Models.DTOs.ProductResultModel;

namespace Backend.Domain.Models.DTOs
{
    public class OrderResultModel
    {
        public Result? Result { get; set; } = null!;

        public class OrderItemModel
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public decimal Total { get; set; }
        }

        public List<OrderItemModel> Orders { get; set; }

        private static OrderItemModel CreateBaseModel(OrderEntity order)
        {
            return new OrderItemModel
            {
                Id = order.Id,
                Date = order.Date,
                Total = order.Total
                //OrderDetails = order.OrderDetail,
            };
        }

        public static OrderResultModel CreateSuccess(OrderEntity order)
        {
            return new OrderResultModel
            {
                Result = Result.Success(),
                Orders = new List<OrderItemModel> { CreateBaseModel(order) }
            };
        }
        public static OrderResultModel CreateFailure(string error)
        {
            return new OrderResultModel
            {
                Result = Result.Failure(new List<string> { error })
            };
        }
    }
}
