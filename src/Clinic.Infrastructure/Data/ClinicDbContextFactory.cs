using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Clinic.Infrastructure.Data
{
    public class ClinicDbContextFactory
        : IDesignTimeDbContextFactory<ClinicDbContext>
    {
        public ClinicDbContext CreateDbContext(string[] args)
        {
            // đọc appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ClinicDbContext>();

            optionsBuilder.UseSqlServer(
                config.GetConnectionString("ClinicDB")
            );

            return new ClinicDbContext(optionsBuilder.Options);
        }
    }
}
