using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using SistemaVuelos.DAL.DbContext;
using Microsoft.EntityFrameworkCore;
//using SistemaVuelos.DAL.interfaces;
//using SistemaVuelos.DAL.Implementacion;
//using SistemaVuelos.BLL.interfaces;
//using SistemaVuelos.Bll.interfaces;

namespace SistemaVuelos.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbNewShoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CadenaSQL"));
            });

            //services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //services.AddScoped<IVentaRepository, VentaRepository>();

            //services.AddScoped<ICorreoService, CorreoService>();
        }
    }
}
