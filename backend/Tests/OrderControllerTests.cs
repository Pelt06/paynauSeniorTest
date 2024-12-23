using Backend.Api.Controllers;
using Backend.Application.Database.Order.Commands.CreateOrder;
using Backend.Application.Database.Order.Models;
using Backend.Application.Database.OrderDetail.Models;
using Backend.Application.External.JwtService;
using Backend.Domain.Entities.Order;
using Backend.Domain.Models;
using Backend.Domain.Models.DTOs;
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
        public async Task Create_ReturnsCreatedResponse_WhenOrderIsValid()
        {
        }

}