using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace QuantityMeasurementRepositoryLayer.Context
{
    public class QuantityMeasurementDbContextFactory : IDesignTimeDbContextFactory<QuantityMeasurementDbContext>
    {
        public QuantityMeasurementDbContext CreateDbContext(string[] args)
        {
            // Read connection string from WebAPI appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "/../QuantityMeasurementWebAPI")
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<QuantityMeasurementDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);

            return new QuantityMeasurementDbContext(optionsBuilder.Options);
        }
    }
}