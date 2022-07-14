using Microsoft.EntityFrameworkCore;
using WebApp.Data.Context;

namespace WebApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(opt =>
            {
                //opt.UseSqlServer(configuration.GetConnectionString("db_almacenContext"));
                var connetionString = configuration.GetConnectionString("DataConection");
                opt.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));

            });
            return services;
        }
    }
}
