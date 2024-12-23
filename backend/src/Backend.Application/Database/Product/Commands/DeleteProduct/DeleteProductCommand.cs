using Backend.Domain.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Product.Commands.DeleteProduct
{
    public class DeleteProductCommand : IDeleteProductCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<DeleteProductCommand> _logger;

        public DeleteProductCommand(IDatabaseService databaseService, ILogger<DeleteProductCommand> logger)
        {
            _databaseService = databaseService;
            _logger = logger;
        }

        public async Task<ProductResultModel> Execute(int Id)
        {
            _logger.LogInformation("Iniciando la eliminación del producto con ID {ProductId}", Id);

            try
            {
                var product = await _databaseService.Products.FirstOrDefaultAsync(x => x.Id == Id);

                if (product == null)
                {
                    _logger.LogWarning("Producto con ID {ProductId} no encontrado. No se puede eliminar.", Id);
                    throw new KeyNotFoundException($"Producto con ID {Id} no encontrado.");
                }

                _logger.LogInformation("Producto encontrado: {ProductName}, eliminando...", product.Name);

                _databaseService.Products.Remove(product);
                _logger.LogInformation("Producto {ProductName} eliminado correctamente.", product.Name);

                await _databaseService.SaveAsync();
                _logger.LogInformation("Cambios guardados correctamente después de eliminar el producto {ProductName}.", product.Name);

                return ProductResultModel.CreateSuccess(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el producto con ID {ProductId}. Detalles del error: {ErrorMessage}", Id, ex.Message);
                return ProductResultModel.CreateFailure(ex.Message);
            }
        }
    }

}
