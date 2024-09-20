using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using Azure;
using SlnMain.Domain;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using SlnMain.Infrastructure;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
using System.Collections;
using SlnMain.Aplication.Services;

namespace SlnMain.Api.Controllers
{

 
        [ApiController]
        [Route("api/Product")]
        public class ProductController : ControllerBase
        {
        private readonly IProductService _productService;

        public ProductController( IProductService productService)
        {
           
     
            _productService = productService;
        }
  
        static readonly HttpClient client = new HttpClient();

        [HttpPost("createProduct")]
        [Authorize]
        public async Task<ActionResult> CreateProduct([FromQuery] string description, decimal price, int ivaPercent, int stock)
        {
            // insert product entity framework
            _productService.CreateProduct( description,  price,  ivaPercent,  stock);
            return Ok();
        }

        [HttpPost("updateProduct")]
        [Authorize]
        public async Task<ActionResult> UpdateProduct([FromQuery] string description, decimal price, int ivaPercent, int stock)
        {
            _productService.UpdateProduct( description,  price,  ivaPercent,  stock);
            // insert product entity framework
            //using (_dbcontext)
            //{
            //    var product = _dbcontext.Set<Product>();
            //    product.Add(new Product { Description = description, Price = price, IvaPercent = ivaPercent, Stock = stock });
            //    _dbcontext.SaveChanges();
            //}
            return Ok();
        }

        [HttpGet("getProducts")]
        [Authorize]
        public async Task<IEnumerable<Product>> GetProducts()
        {
               //Execute function in repository
            var listOfProducts = _productService.GetProducts();
            return listOfProducts;

        }

        }

}