using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SlnMain.Api.Controllers;
using SlnMain.Aplication.Services;
using SlnMain.Infrastructure;

namespace MainUnitTesting
{
    public class OrderTesting 
    {

        private readonly OrderController _controller;
        private readonly DbCarvajalContext _dbcontext;
        private readonly IOrderService _service;

        public OrderTesting()
        {
  
            _dbcontext = new DbCarvajalContext();
            _service = new OrderService(_dbcontext);
            _controller = new OrderController(_service);
            
        }

        [Fact]
        public void Get_Ok_Header()
        {
            var result = _controller.GetOrderHeader();
            Assert.NotNull(result);

        }

        [Fact]
        public void Get_Ok_Details()
        {
            var result = _controller.GetOrderDetails(15);
            Assert.NotNull(result);

        }

    }
}