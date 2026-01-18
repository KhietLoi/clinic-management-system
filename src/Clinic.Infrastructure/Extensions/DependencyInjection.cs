using Clinic.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Clinic.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var cs = config.GetConnectionString("ClinicDB") ?? "";

            // ép SSL options cho local SQL Express
            if (!cs.Contains("Encrypt=", StringComparison.OrdinalIgnoreCase))
                cs += (cs.EndsWith(";") ? "" : ";") + "Encrypt=True;";
            if (!cs.Contains("TrustServerCertificate=", StringComparison.OrdinalIgnoreCase))
                cs += "TrustServerCertificate=True;";

            services.AddDbContext<ClinicDbContext>(options => options.UseSqlServer(cs));
            return services;
        }
    }
}
