using Backend.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Product.Queries.GetProducts
{
    public interface IGetProductsQuery
    {
        Task<ProductResultModel> Execute();
    }
}
