using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SlnPactia.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using Azure;
using SlnPactia.Domain;
using System.Text;
using SlnPactia.Aplication.Repository;
using Microsoft.AspNetCore.Authorization;

namespace SlnPactia.Api.Controllers
{

 
        [ApiController]
        [Route("api/task")]
        public class TaskController : ControllerBase
        {

        ListOfTaskRepository listOfTaskRepository;
        private readonly DbPactiaContext _dbcontext;


        public TaskController(DbPactiaContext dbcontext)
        {
           
            _dbcontext = dbcontext;
        }
  
        static readonly HttpClient client = new HttpClient();


        [HttpPost("updateTask")]
        //[Authorize]
        public async Task<ActionResult<List<ListOfTask>>> updateTask([FromQuery] int id, string status)
        {
            //Updates a task as long as it does not exceed COMPLETADO to PENDING
            listOfTaskRepository = new ListOfTaskRepository(_dbcontext);
            listOfTaskRepository.updateListOfTask(id, status);
            return Ok();
          

        }


        [HttpPost("createTask")]
        //[Authorize]
        public async Task<ActionResult<List<ListOfTask>>> createTask([FromQuery] string nameOfTask)
        {
            //Create a task automatically assigning it to status PENDIENTE
            listOfTaskRepository = new ListOfTaskRepository(_dbcontext);
            listOfTaskRepository.createListOfTask(nameOfTask);
            return Ok();


        }

     


        [HttpPost("deleteTask")]
        //[Authorize]
        public async Task<ActionResult<List<ListOfTask>>> deleteTask([FromQuery] int id)
        {
            //deletes a task as long as it is not COMPLETADA
            listOfTaskRepository = new ListOfTaskRepository(_dbcontext);
            listOfTaskRepository.deleteListOfTask(id);
            return Ok();

        }

        [HttpGet("getTask")]
        
        public async Task<ActionResult<List<ListOfTask>>> getTask()
        {
            listOfTaskRepository = new ListOfTaskRepository(_dbcontext);
            return listOfTaskRepository.getListOfTasks();
           

        }





    }

}