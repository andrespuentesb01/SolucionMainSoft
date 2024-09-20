using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Azure;
using SlnMain.Domain;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using SlnMain.Infrastructure;
using SlnMain.Domain.Models;
using System.Security.Cryptography;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using Azure.Core;
using SlnMain.Aplication.Services;

namespace SlnMain.Api.Controllers
{

 
        [ApiController]
        [Route("api/order")]
        public class OrderController : ControllerBase
        {

 
        private readonly IOrderService _orderService;
 

        public OrderController(IOrderService orderService)
        {

            _orderService = orderService;
       
        }
  
        static readonly HttpClient client = new HttpClient();

        [HttpPost("createOrder")]
        [Authorize]
        public async Task<ActionResult> CreateOrder([FromBody] List<OrderDetails> orderDetailList)
        {
            //Execute the repository function to list.
            string jsonString = JsonConvert.SerializeObject(orderDetailList);
            var listRent = _orderService.CreateOrder(jsonString);
            return Ok();
        }



        [HttpGet("getOrderHeader")]
        [Authorize]
        public async Task<ActionResult<List<OrderHeader>>> GetOrderHeader()
        {
            //Execute the entity framework function to list.
            var listOfHeader = _orderService.GetOrderHeaders();
            return Ok(listOfHeader);

        }

        [HttpGet("getOrderDetails")]
        [Authorize]
        public async Task<ActionResult<List<OrderDetail>>> GetOrderDetails([FromQuery] int id)
        {
            //Execute the entity framework function to list.
            var listOfDetails = _orderService.getOrderDetails(id);
            return Ok(listOfDetails);

        }

        }

}