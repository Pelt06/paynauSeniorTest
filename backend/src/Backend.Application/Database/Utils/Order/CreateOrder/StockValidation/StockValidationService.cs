using Backend.Application.Database.Order.Commands.CreateOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Utils.Order.CreateOrder.StockValidation
{
    public class StockValidationService : IStockValidationService
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<CreateOrderCommand> _logger;

        public StockValidationService(IDatabaseService databaseService, ILogger<CreateOrderCommand> logger)
        {
            _databaseService = databaseService;
            _logger = logger;
        }

        public async Task<bool> ValidateStockAsync(int productId, int quantity)
        {
            var product = await _databaseService.Products.FindAsync(productId);

            if (product == null)
            {
                _logger.LogError("Producto con ID {ProductId} no encontrado.", productId);
                throw new Exception($"Producto con ID {productId} no encontrado.");
            }

            if (product.Stock < quantity)
            {
                _logger.LogError("No hay suficiente stock para el producto {ProductName}. Stock disponible: {AvailableStock}, cantidad solicitada: {RequestedQuantity}",
                    product.Name, product.Stock, quantity);
                throw new Exception($"No hay suficiente stock para el producto {product.Name}. Stock disponible: {product.Stock}, cantidad solicitada: {quantity}.");
            }

            product.Stock -= quantity;
            try
            {
                _databaseService.Products.Update(product);
                await _databaseService.SaveAsync();
                _logger.LogInformation("Stock actualizado para el producto {ProductName}. Stock restante: {RemainingStock}", product.Name, product.Stock);
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError("El stock del producto {ProductName} ha cambiado mientras se procesaba la orden.", product.Name);
                throw new Exception($"El stock del producto {product.Name} ha cambiado mientras se procesaba la orden. Por favor, intente nuevamente.");
            }

            return product?.Stock >= quantity;
        }
    }

}
