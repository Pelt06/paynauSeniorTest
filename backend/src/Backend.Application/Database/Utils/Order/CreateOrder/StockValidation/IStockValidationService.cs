using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Utils.Order.CreateOrder.StockValidation
{
    public interface IStockValidationService
    {
        Task<bool> ValidateStockAsync(int productId, int quantity);
    }
}
