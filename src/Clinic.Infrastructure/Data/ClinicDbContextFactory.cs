using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Clinic.Infrastructure.Data
{
    public class ClinicDbContextFactory : IDesignTimeDbContextFactory<ClinicDbContext>
    {
        public ClinicDbContext CreateDbContext(string[] args)
        {
            // trỏ thẳng sang Clinic.Api để lấy appsettings đúng
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Clinic.Api");

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
              
                .Build();

            var cs = config.GetConnectionString("ClinicDB");

            var optionsBuilder = new DbContextOptionsBuilder<ClinicDbContext>();
            optionsBuilder.UseSqlServer(cs);

            return new ClinicDbContext(optionsBuilder.Options);
        }
    }
}
