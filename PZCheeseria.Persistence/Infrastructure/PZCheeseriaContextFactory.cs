using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PZCheeseria.Domain;

namespace PZCheeseria.Persistence.Infrastructure
{
    public class PZCheeseriaContextFactory:IDesignTimeDbContextFactory<PZCheeseriaContext>
    {
        private const string ConnectionStringName = "PZCheeseriaConnectionString";

        
        public PZCheeseriaContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}PZCheeseria.Api", Path.DirectorySeparatorChar);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            var optionsBuilder = new DbContextOptionsBuilder<PZCheeseriaContext>();
            
            optionsBuilder.UseSqlServer(connectionString);

            return new PZCheeseriaContext(optionsBuilder.Options);
        }
    }
}