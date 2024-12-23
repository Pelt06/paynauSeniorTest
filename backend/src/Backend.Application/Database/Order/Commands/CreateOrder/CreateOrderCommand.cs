using AutoMapper;
using Backend.Application.Database.Order.Models;
using Backend.Application.Database.Utils.Order.CreateOrder.StockValidation;
using Backend.Application.Database.Utils.Order.CreateOrder.TotalCalculator;
using Backend.Application.External.JwtService;
using Backend.Domain.Entities.Order;
using Backend.Domain.Entities.OrderDetail;
using Backend.Domain.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Order.Commands.CreateOrder
{
    public class CreateOrderCommand : ICreateOrderCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<CreateOrderCommand> _logger;
        private readonly IStockValidationService _stockValidationService;
        private readonly ITotalCalculatorService _totalCalculatorService;

        public CreateOrderCommand(IDatabaseService databaseService, ILogger<CreateOrderCommand> logger, IStockValidationService stockValidationService, ITotalCalculatorService totalCalculatorService)
        {
            _databaseService = databaseService;
            _logger = logger;
            _stockValidationService = stockValidationService;
            _totalCalculatorService = totalCalculatorService;
        }

        public async Task<OrderResultModel> Execute(CreateOrderModel model)
        {
            _logger.LogInformation("Iniciando la creación de la orden. Detalles de la orden: {@OrderModel}", model);

            var executionStrategy = _databaseService.db.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _databaseService.BeginTransactionAsync();
                _logger.LogInformation("Transacción iniciada para la creación de la orden.");

                try
                {
                    foreach (var orderDetail in model.OrderDetails)
                    {
                        _logger.LogInformation("Procesando detalle de la orden: ProductoId = {ProductId}, Cantidad = {Quantity}", orderDetail.ProductId, orderDetail.Quantity);
                        await _stockValidationService.ValidateStockAsync(orderDetail.ProductId, orderDetail.Quantity);
                    }

                    var total = _totalCalculatorService.CalculateTotal(model.OrderDetails, _databaseService);

                    var order = new OrderEntity
                    {
                        Date = DateTime.UtcNow,
                        Total = total
                    };

                    await _databaseService.Orders.AddAsync(order);
                    await _databaseService.SaveAsync();

                    _logger.LogInformation("Orden creada exitosamente. Total: {TotalAmount}, Fecha: {OrderDate}", order.Total, order.Date);

                    foreach (var orderDetail in model.OrderDetails)
                    {
                        var orderDetailEntity = new OrderDetailEntity
                        {
                            OrderId = order.Id,
                            ProductId = orderDetail.ProductId,
                            Quantity = orderDetail.Quantity
                        };
                        await _databaseService.OrderDetails.AddAsync(orderDetailEntity);
                    }

                    await _databaseService.SaveAsync();
                    await transaction.CommitAsync();

                    _logger.LogInformation("Transacción completada exitosamente para la orden ID: {OrderId}", order.Id);

                    return OrderResultModel.CreateSuccess(order);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al procesar la creación de la orden.");
                    await transaction.RollbackAsync();
                    return OrderResultModel.CreateFailure(ex.Message);
                }
            });
        }

    }

}
