using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using SlnPactia.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
//using SlnPactia.DAL.interfaces;
//using SlnPactia.DAL.Implementacion;
//using SlnPactia.BLL.interfaces;
//using SlnPactia.Bll.interfaces;

namespace SlnPactia.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbPactiaContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CadenaSQL"));
            });

            //services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //services.AddScoped<IVentaRepository, VentaRepository>();

            //services.AddScoped<ICorreoService, CorreoService>();
        }
    }
}
