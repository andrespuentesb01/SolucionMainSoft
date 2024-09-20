using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using Azure;
using SlnMain.Domain;
using SlnMain.Domain.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using SlnMain.Infrastructure;
using Azure.Core;
using AutoMapper;
using SlnMain.Api.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Net;
using SlnMain.Aplication.Services;

namespace SlnMain.Api.Controllers
{


    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {


        private readonly IUserService _userService; 
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        static readonly HttpClient client = new HttpClient();
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public UserController(ILogger<UserController> logger, IMapper mapper, IUserRepository userRepository, IUserService userService)
        {

           
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
            _userRepository = userRepository;
            _response = new();
        }


        [HttpPost("createUser")]
        [Authorize]
        public async Task<ActionResult<List<User>>> createUser([FromQuery] string name, string password)
        {
            // insert user entity framework

            _userService.CreateUser( name,  password);

            return Ok(new List<User>());
        }

        [HttpGet("getUsers")]
        [Authorize]
        public async Task<ActionResult<List<User>>> getUsers()
        {
            _logger.LogInformation("obtener los usuarios");
            //Execute entity framework function
            var listOfUsers = _userService.GetUsers();

            return Ok(listOfUsers);


        }

        //-----------------------------------------------------------------respuestas y peticiones -------------------------------

        [HttpGet("getUsers/id", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize]
        public async Task <ActionResult<APIResponse>> getUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("el id es incorrecto");
                    return BadRequest();
                }
                //Execute entity framework function
                var listOfUsers = _userRepository.Obtener(c => c.Id == id);


                if (listOfUsers == null)
                {
                    return NotFound();
                }
                _response.Resultado = _mapper.Map<UsuarioDto>(User);
                _response.statusCode = HttpStatusCode.OK;
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch
            (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessage =new List<string>() { ex.Message };

            }
            return _response;
        }


        [HttpPost("createUserComplete")]//siempre se crea en post
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task <ActionResult<UsuarioDto>> createUserCompleted([FromBody] UsuarioDto usuarioDto)
        {
            // insert user entity framework
            if (usuarioDto == null)
            {
                return BadRequest(usuarioDto);

            }
            if (!ModelState.IsValid) 
            {
                return BadRequest(usuarioDto);

            }
            if (await _userRepository.Obtener(v=>v.Name.ToLower() == usuarioDto.mail.ToLower())!= null)
            {
                ModelState.AddModelError("NombreExiste", "el usuario ya existe");
                return BadRequest(ModelState);
            }

           
                string result = string.Empty;
                byte[] encryted = Encoding.ASCII.GetBytes(usuarioDto.password);
                result = Convert.ToBase64String(encryted);
            _userService.saveChangesEncrypted(usuarioDto, result);
            
            int id1 = 1;
            return CreatedAtRoute("GetUser", new {id= id1}, usuarioDto);
            //return Ok(usuarioDto);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize]
        public async Task <ActionResult> DeleteUsuario(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var usuario = _userService.Get(id);
            if (usuario == null)
            {
                
                return NotFound();
            }
            _userService.Remove(usuario);
            return NoContent();
        }

        [HttpPut("{id:int}")]//el patch es solo una propiedad de todo el objeto
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize]
        public async Task<ActionResult> UpdateUsuario(int id, [FromBody]UsuarioDto usuarioDto)
        {
            if ((id == 0) || (usuarioDto == null))
            {
                return BadRequest();
            }
            var usuario = _userService.Get(id);      
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Name = usuarioDto.mail;
            usuario.Password = usuarioDto.password;

       
            _userService.Update(usuario);
            return NoContent();
        }

        [HttpPatch("{id:int}")]//el patch es solo una propiedad de todo el objeto
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize]
        public async Task<ActionResult> UpdatePartialUsuario(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<UsuarioDto> patchDto)
        {
            if ((id == 0) || (patchDto == null))
            {
                return BadRequest();
            }
            var usuario = _userService.Get(id);

            UsuarioDto usuarioDto = new()
            {
                mail = usuario.Name,
                password = usuario.Password
            };
            if (usuario == null)
            {
                return NotFound();
            }
            patchDto.ApplyTo(usuarioDto);

            
            return NoContent();
        }
        //-----------------------------------------------------------------respuestas y peticiones -------------------------------
        [HttpDelete("deleteUser")]
        [Authorize]
        public async Task<ActionResult<Boolean>> deleteUsers([FromQuery] int id)
        {
            try
            {
                //Execute entity framework function
                _userService.deleteUsers(id);
                
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(false);

            }
        }

        [HttpPut("updateUser")]
        [Authorize]
        public async Task<ActionResult<Boolean>> updateUsers([FromQuery] int id, string name)
        {
            try
            {
                //Execute entity framework function
                _userService.updateUsers(id, name);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(false);

            }
        }

    }

}