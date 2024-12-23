using Backend.Application.Database.Product.Commands.CreateProduct;
using Backend.Application.Database.Product.Commands.DeleteProduct;
using Backend.Application.Database.Product.Commands.UpdateProduct;
using Backend.Application.Database.Product.Models;
using Backend.Application.Database.Product.Queries.GetProducts;
using Backend.Application.Exceptions;
using Backend.Application.Features;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    [Authorize]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductModel model,
            [FromServices] ICreateProductCommand createCommand)
        {
            var data = await createCommand.Execute(model);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateProductModel model,
            [FromServices] IUpdateProductCommand updateCommand)
        {            
            var data = await updateCommand.Execute(model);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete(
            int Id,
            [FromServices] IDeleteProductCommand deleteCommand)
        {
            if(Id == 0) 
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await deleteCommand.Execute(Id);
            if(!data.Result.Succeeded)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound, data));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));

        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromServices] IGetProductsQuery getQuery)
        {
            var data = await getQuery.Execute();

            if(data == null)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound, data));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));

        }
    }
}
