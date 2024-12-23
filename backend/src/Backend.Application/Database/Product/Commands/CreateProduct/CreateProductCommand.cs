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

namespace Backend.Application.Database.Product.Commands.CreateProduct
{

    public class CreateProductCommand : ICreateProductCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommand> _logger;

        public CreateProductCommand(IDatabaseService databaseService, IMapper mapper, ILogger<CreateProductCommand> logger)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductResultModel> Execute(CreateProductModel model)
        {
            _logger.LogInformation("Iniciando la creación del producto. Detalles del producto: {@ProductModel}", model);

            try
            {
                var product = _mapper.Map<ProductEntity>(model);
                _logger.LogInformation("Producto mapeado con éxito. Nombre: {ProductName}, Precio: {ProductPrice}", product.Name, product.Price);

                await _databaseService.Products.AddAsync(product);
                _logger.LogInformation("Producto {ProductName} agregado a la base de datos.", product.Name);

                await _databaseService.SaveAsync();
                _logger.LogInformation("Producto {ProductName} guardado con éxito.", product.Name);

                return ProductResultModel.CreateSuccess(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el producto. Detalles del error: {ErrorMessage}", ex.Message);
                return ProductResultModel.CreateFailure(ex.Message);
            }
        }
    }

}
