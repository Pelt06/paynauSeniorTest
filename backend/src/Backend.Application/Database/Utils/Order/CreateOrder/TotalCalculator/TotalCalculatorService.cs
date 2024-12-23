using Backend.Application.Database.OrderDetail.Models;
using Backend.Domain.Entities.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Utils.Order.CreateOrder.TotalCalculator
{
    public class TotalCalculatorService : ITotalCalculatorService
    {
        public decimal CalculateTotal(List<CreateOrderDetailModel> orderDetails, IDatabaseService databaseService)
        {
            return orderDetails.Sum(od => od.Quantity * databaseService.Products.First(p => p.Id == od.ProductId).Price);
        }
    }
}
