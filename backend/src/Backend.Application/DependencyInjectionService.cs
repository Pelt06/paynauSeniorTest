using AutoMapper;
using Backend.Application.Configuration;
using Backend.Application.Database.Order.Commands.CreateOrder;
using Backend.Application.Database.Product.Commands.CreateProduct;
using Backend.Application.Database.Product.Commands.DeleteProduct;
using Backend.Application.Database.Product.Commands.UpdateProduct;
using Backend.Application.Database.Product.Queries.GetProducts;
using Backend.Application.Database.Utils.Order.CreateOrder.StockValidation;
using Backend.Application.Database.Utils.Order.CreateOrder.TotalCalculator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
            });

            services.AddSingleton(mapper.CreateMapper());
            services.AddTransient<ICreateProductCommand, CreateProductCommand>();
            services.AddTransient<IUpdateProductCommand, UpdateProductCommand>();
            services.AddTransient<IDeleteProductCommand, DeleteProductCommand>();
            services.AddTransient<IGetProductsQuery, GetProductsQuery>();

            services.AddTransient<ICreateOrderCommand, CreateOrderCommand>();
            services.AddTransient<IStockValidationService, StockValidationService>();
            services.AddTransient<ITotalCalculatorService, TotalCalculatorService>();

            return services;
        }
    }
}
