using Backend.Application.Database.Order.Commands.CreateOrder;
using Backend.Application.Database.Order.Models;
using Backend.Application.Database.Product.Commands.CreateProduct;
using Backend.Application.Database.Product.Models;
using Backend.Application.Database.Product.Queries.GetProducts;
using Backend.Application.Exceptions;
using Backend.Application.External.JwtService;
using Backend.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [Route("api/v1/order")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IGetJwtService _jwtTokenGenerator;

        public OrderController(IGetJwtService jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [AllowAnonymous]
        [HttpGet("get-jwt")]
        public async Task<IActionResult> GetJwt()
        {
            var jwt = _jwtTokenGenerator.GenerateToken();

            return Ok(new
            {
                Token = jwt,
            });

        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateOrderModel model,
            [FromServices] ICreateOrderCommand createCommand)
        {
            var data = await createCommand.Execute(model);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }
    }
}
