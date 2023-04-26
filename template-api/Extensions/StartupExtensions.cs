using Backend.Data;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddThirdPartyServices(
            this IServiceCollection services
            )
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            Console.WriteLine("--> Using InMem Db");
            services.AddDbContext<AppDbContext>(opt =>
                 opt.UseInMemoryDatabase("InMemoryDB"));

            
            services.AddScoped<IRepository<Backend.Models.Task>, Repository<Backend.Models.Task>>();

            return services;
        }
    }
}