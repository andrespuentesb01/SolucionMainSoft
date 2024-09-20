using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlnMain.Api.Controllers;
using SlnMain.Aplication.Services;
using SlnMain.Infrastructure;

namespace MainUnitTesting
{
    public class ProductTesting 
    {

        private readonly ProductController _controller;
        private readonly DbCarvajalContext _dbcontext;
        private readonly IProductService _service;

        public ProductTesting()
        {
            _dbcontext = new DbCarvajalContext();
            _service = new ProductService(_dbcontext);
            _controller = new ProductController(_service);
            
        }

        [Fact]
        public void Get_Ok()
        {
            var result = _controller.GetProducts();
            Assert.NotNull(result);

        }


    }
}