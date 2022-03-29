using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DaNangBayBooking.Data.EF
{
    public class DaNangDbContextFactory : IDesignTimeDbContextFactory<DaNangDbContext>
    {
        public DaNangDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DaNangBayDb");

            var optionsBuilder = new DbContextOptionsBuilder<DaNangDbContext>();
            optionsBuilder.UseSqlServer(connectionString);


            return new DaNangDbContext(optionsBuilder.Options);
        }
    }
}
