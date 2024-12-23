using Backend.Application.Database.Product.Models;
using Backend.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Product.Commands.CreateProduct
{
    public interface ICreateProductCommand
    {
        Task<ProductResultModel> Execute(CreateProductModel model);
    }
}
