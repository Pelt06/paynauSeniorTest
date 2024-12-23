using Backend.Application.Database.Order.Models;
using Backend.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Database.Order.Commands.CreateOrder
{
    public interface ICreateOrderCommand
    {
        Task<OrderResultModel> Execute(CreateOrderModel model);
    }
}
