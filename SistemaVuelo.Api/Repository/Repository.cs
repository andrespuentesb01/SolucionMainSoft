using Microsoft.EntityFrameworkCore;
using SlnMain.Api.Repository.IRepository;
using SlnMain.Infrastructure;
using System.Linq.Expressions;

namespace SlnMain.Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly DbCarvajalContext _db;
        internal DbSet<T> dbSet;

        public Repository(DbCarvajalContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();  
        }
        public async Task Crear(T entidad)
        {
            await _db.AddAsync(entidad);
            await Grabar();
        }

        public async Task Grabar()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked) 
            { 
            query =query.AsNoTracking();
            }
            if (filtro != null) 
            {

                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {

                query = query.Where(filtro);
            }
            return await query.ToListAsync();   

        }

        public async Task Remover(T entidad)
        {
            dbSet.Remove(entidad);
            await Grabar();
        }
    }
}
