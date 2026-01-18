using Clinic.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Clinic.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        //ket noi DB:
        public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ClinicDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("ClinicDB")));
            return services;
        }
    }
}
