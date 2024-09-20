using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using SlnMain.Infrastructure;
//using SlnMain.DAL.interfaces;
//using SlnMain.DAL.Implementacion;
//using SlnMain.BLL.interfaces;
//using SlnMain.Bll.interfaces;

namespace SlnMain.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbCarvajalContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CadenaSQL"));
            });

            //services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //services.AddScoped<IVentaRepository, VentaRepository>();

            //services.AddScoped<ICorreoService, CorreoService>();
        }
    }
}
