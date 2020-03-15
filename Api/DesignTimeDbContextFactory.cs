using Microsoft.EntityFrameworkCore.Design;
using DataModel.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Api
{
    // public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    // {
    //     public ApplicationContext CreateDbContext(string[] args)
    //     {
    //         IConfigurationRoot configuration = new ConfigurationBuilder()
    //             .SetBasePath(Directory.GetCurrentDirectory())
    //             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
    //             .Build();
    //         var builder = new DbContextOptionsBuilder<ApplicationContext>();
    //         var connectionString = configuration.GetConnectionString("sqlConString");
    //         builder.UseSqlServer(connectionString);
    //         return new ApplicationContext(builder.Options);
    //     }
    // }
}