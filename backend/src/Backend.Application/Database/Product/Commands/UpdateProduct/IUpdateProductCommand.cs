using Backend.Application.Database.Product.Models;
using Backend.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Product.Commands.UpdateProduct
{
    public interface IUpdateProductCommand
    {
        Task<ProductResultModel> Execute(UpdateProductModel model);
    }
}
