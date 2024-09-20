using SlnMain.Domain;
using SlnMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnMain.Aplication.Services
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();
        public User Get(int id);
        public void CreateUser(string name, string password);
        public void Remove(User usuario);
        public void Update(User usuario);
        public void updateUsers(int id, string name);
        public void saveChangesEncrypted(UsuarioDto usuarioDto, string result);
        public void deleteUsers(int id);

        public string validateUser(UsuarioDto request);
        
    }
}
