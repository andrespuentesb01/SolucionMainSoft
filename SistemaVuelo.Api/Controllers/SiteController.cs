using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using Azure;
using SlnMain.Domain;
using System.Text;
using SlnMain.Aplication.Repository;
using Microsoft.AspNetCore.Authorization;
using SlnMain.Infrastructure;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using SlnMain.Domain.Models;

namespace SlnMain.Api.Controllers
{

 
        [ApiController]
        [Route("api/site")]
        public class SiteController : ControllerBase
        {

        SiteRepository siteRepository;
        private readonly DbRentCarContext _dbcontext;
        private readonly IConfiguration configuration;

        public SiteController(DbRentCarContext dbcontext, IConfiguration _configuration)
        {
           
            _dbcontext = dbcontext;
            configuration = _configuration;
        }
  
        static readonly HttpClient client = new HttpClient();



        [HttpPost("createSite")]
        [Authorize]
        public async Task<ActionResult<List<Site>>> createSite([FromQuery] int idCar, int idDelivery, int idCollect)
        {
            //Execute the repository function to list.
            siteRepository = new SiteRepository(_dbcontext, configuration);
            var listSites = siteRepository.CreateSite(idCar, idDelivery, idCollect);
            return Ok(listSites);
        }



        [HttpGet("getSite")]
        [Authorize]
        public async Task<ActionResult<List<Site>>> getSite()
        {
            //Execute the entity framework function to list.
            var listOfSites = _dbcontext.Sites.ToList();
            return listOfSites;

        }

        [HttpGet("getSiteDetails")]
        [Authorize]
        public async Task<ActionResult<List<SiteDetails>>> getSiteDetails()
        {
            //Execute the repository function to list.
            siteRepository = new SiteRepository(_dbcontext, configuration);
            var listSites = siteRepository.GetSiteDetails();
            return Ok(listSites);

        }

    }

}