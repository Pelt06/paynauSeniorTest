using Backend.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Models.DTOs;

public class ProductResultModel
{
    public Result? Result { get; set; } = null!;

    public class ProductItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
    public IEnumerable<ProductItemModel>? Products { get; set; } = null!;

    private static ProductItemModel CreateBaseModel(ProductEntity product)
    {
        return new ProductItemModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
    }

    public static ProductResultModel CreateSuccess(ProductEntity product)
    {
        return new ProductResultModel
        {
            Result = Result.Success(),
            Products = new List<ProductItemModel> { CreateBaseModel(product) }
        };
    }

    public static ProductResultModel GetAllProducts(IEnumerable<ProductEntity> products)
    {
        return new ProductResultModel
        {
            Result = Result.Success(),
            Products = products.Select(CreateBaseModel)
        };
    }

    public static ProductResultModel CreateFailure(string error)
    {
        return new ProductResultModel
        {
            Result = Result.Failure(new List<string> { error })
        };
    }
}


