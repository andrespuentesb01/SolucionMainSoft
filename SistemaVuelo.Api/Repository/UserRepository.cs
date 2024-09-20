using SlnMain.Api.Repository.IRepository;
using SlnMain.Domain;
using SlnMain.Infrastructure;

namespace SlnMain.Api.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbCarvajalContext _db;
        public UserRepository(DbCarvajalContext db) : base(db)
        {
            _db = db;
        }

        public async Task<User> Actualizar(User entidad)
        {
           //entidad.Name = string.Empt
                _db.Users.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
