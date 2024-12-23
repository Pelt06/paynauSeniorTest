using Backend.Api.Controllers;
using Backend.Application.Database.Order.Commands.CreateOrder;
using Backend.Application.Database.Order.Models;
using Backend.Application.Database.OrderDetail.Models;
using Backend.Application.External.JwtService;
using Backend.Domain.Entities.Order;
using Backend.Domain.Models;
using Backend.Domain.Models.DTOs;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class OrderControllerTests
    {
        private readonly Mock<IGetJwtService> _mockJwtService;
        private readonly Mock<ICreateOrderCommand> _mockCreateOrderCommand;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _mockJwtService = new Mock<IGetJwtService>();
            _mockCreateOrderCommand = new Mock<ICreateOrderCommand>();
            _controller = new OrderController(_mockJwtService.Object);
        }

    [Fact]
    public async Task GetJwt_ReturnsToken()
    {
        // Arrange
        var token = "test-token";
        _mockJwtService.Setup(service => service.GenerateToken()).Returns(token);

        // Act
        var result = await Task.FromResult(_controller.GetJwt());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<dynamic>(okResult.Value);
        Assert.Equal(token, response.Token);
    }

    [Fact]
    public async Task Create_Returns201Created()
    {
        // Arrange
        var createOrderModel = new CreateOrderModel
        {
            OrderDetails = new List<CreateOrderDetailModel>
        {
            new CreateOrderDetailModel { ProductId = 1, Quantity = 2 },
            new CreateOrderDetailModel { ProductId = 2, Quantity = 3 }
        }
        };

        var responseData = OrderResultModel.CreateSuccess(new OrderEntity
        {
            Id = 1,
            Date = DateTime.UtcNow,
            Total = 100.50m
        });

        _mockCreateOrderCommand.Setup(command => command.Execute(createOrderModel)).ReturnsAsync(responseData);

        // Act
        var result = await _controller.Create(createOrderModel, _mockCreateOrderCommand.Object);

        // Assert
        var createdResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);

        var response = Assert.IsType<BaseResponseModel>(createdResult.Value);
        Assert.True(response.Success);
        Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
        Assert.Equal(responseData, response.Data);
    }


    [Fact]
    public async Task Create_Returns500OnException()
    {
        // Arrange
        var createOrderModel = new CreateOrderModel
        {
            OrderDetails = new List<CreateOrderDetailModel>
        {
            new CreateOrderDetailModel { ProductId = 1, Quantity = 2 }
        }
        };

        _mockCreateOrderCommand.Setup(command => command.Execute(createOrderModel)).ThrowsAsync(new Exception("Test Exception"));

        // Act
        var result = await _controller.Create(createOrderModel, _mockCreateOrderCommand.Object);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        var response = Assert.IsType<BaseResponseModel>(objectResult.Value);
        Assert.False(response.Success);
        Assert.Equal("Test Exception", response.Message);
    }


}