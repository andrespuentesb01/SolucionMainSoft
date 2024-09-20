using Azure;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

//using SlnMain.Infrastructure.DbContext;
using SlnMain.Domain;
using SlnMain.Domain.Models;
using SlnMain.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SlnMain.Aplication.Services
{
    public class UserService: IUserService
    {

        private readonly DbCarvajalContext _dbcontext;
        private readonly IConfiguration configuration;
        private readonly string secretKey;
        public UserService(DbCarvajalContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext;
            this.configuration = configuration;
            secretKey = configuration.GetSection("settings").GetSection("secreteky").ToString();
        }

        public async void CreateUser( string name, string password)
        {
            // insert user entity framework
            string result = string.Empty;
            byte[] encryted = Encoding.ASCII.GetBytes(password);
            result = Convert.ToBase64String(encryted);
            var user = _dbcontext.Set<User>();
            user.Add(new User { Name = name, Password = result});
            _dbcontext.SaveChanges();


        }

        public async void Remove(User usuario)
        {
            _dbcontext.Users.Remove(usuario);

        }

        public async void Update(User usuario)
        {
            _dbcontext.Users.Update(usuario);
            await _dbcontext.SaveChangesAsync();

        }

        public async void updateUsers( int id, string name)
        {
           
                //Execute entity framework function
              
             var entidad = _dbcontext.Users.Where(w => w.Id == id).SingleOrDefault();
            if (entidad != null)
            {
                entidad.Name = name;
                await _dbcontext.SaveChangesAsync();
            }
                
        }


        public async void deleteUsers( int id)
        {

            //Execute entity framework function
            var user = this.Get(id);
            if(user != null) 
            { 
                _dbcontext.Users.Attach(user);
                _dbcontext.Users.Remove(user);
                _dbcontext.SaveChanges();
            }

        }

        public async void saveChangesEncrypted(UsuarioDto usuarioDto, string result)
        {

            var user = _dbcontext.Set<User>();
            user.AddAsync(new User { Name = usuarioDto.mail, Password = result });
            _dbcontext.SaveChangesAsync();


        }


        public string validateUser(UsuarioDto request)
        {
            if (validateUserDb(request) == true)
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.mail));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string tokenCreato = tokenHandler.WriteToken(tokenConfig);
                return tokenCreato;
            }
            return "";

        }

        public Boolean validateUserDb(UsuarioDto request)
        {
            string result = string.Empty;
            byte[] encryted = Encoding.ASCII.GetBytes(request.password);
            result = Convert.ToBase64String(encryted);
            var listOfUsers2 = _dbcontext.Users.Where(c => c.Name == request.mail)
                                 .Where(c => c.Password.Contains(result))
                                 .Select(c => c.Name);
            if (listOfUsers2.Count() > 0)
            {
                return true;
            }
            return false;

        }

        public IEnumerable<User> GetUsers()
        {
            var listOfUsers = _dbcontext.Users.ToList();
            return listOfUsers;
        }

        public User Get(int id)
        {
            var user = _dbcontext.Users.FirstOrDefault(d => d.Id == id);
            return user;
        }
    }
}