using SlnMain.Domain;

namespace SlnMain.Api.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Actualizar(User entidad);
    }
}
