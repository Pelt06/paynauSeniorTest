using AutoMapper;
using Backend.Application.Database.Product.Models;
using Backend.Domain.Entities.Product;
using Backend.Domain.Models.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Product.Commands.UpdateProduct
{
    public class UpdateProductCommand : IUpdateProductCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductCommand> _logger;

        public UpdateProductCommand(IDatabaseService databaseService, IMapper mapper, ILogger<UpdateProductCommand> logger)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductResultModel> Execute(UpdateProductModel model)
        {
            _logger.LogInformation("Iniciando la actualización del producto con ID {ProductId}", model.Id);

            try
            {
                var product = _mapper.Map<ProductEntity>(model);

                _databaseService.Products.Update(product);
                await _databaseService.SaveAsync();

                _logger.LogInformation("Producto con ID {ProductId} actualizado correctamente.", model.Id);

                return ProductResultModel.CreateSuccess(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el producto con ID {ProductId}. Detalles del error: {ErrorMessage}", model.Id, ex.Message);
                return ProductResultModel.CreateFailure(ex.Message);
            }
        }
    }

}
