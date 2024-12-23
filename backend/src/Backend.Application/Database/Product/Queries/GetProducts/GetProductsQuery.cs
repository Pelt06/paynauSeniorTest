using AutoMapper;
using Backend.Domain.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Product.Queries.GetProducts
{
    public class GetProductsQuery : IGetProductsQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductsQuery> _logger;

        public GetProductsQuery(IDatabaseService databaseService, IMapper mapper, ILogger<GetProductsQuery> logger)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductResultModel> Execute()
        {
            _logger.LogInformation("Iniciando la consulta para obtener todos los productos.");

            try
            {
                var products = await _databaseService.Products.ToListAsync();

                _logger.LogInformation("Se obtuvieron {ProductCount} productos.", products.Count);

                return ProductResultModel.GetAllProducts(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos. Detalles del error: {ErrorMessage}", ex.Message);
                return ProductResultModel.CreateFailure(ex.Message);
            }
        }
    }

}
