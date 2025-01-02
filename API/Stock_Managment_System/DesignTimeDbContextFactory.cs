namespace Stock_Managment_System
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using SMS.Data; // Ensure this is the correct namespace for your DbContext
    using System.IO;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SMSDbContext>
    {
        public SMSDbContext CreateDbContext(string[] args)
        {
            // Build the configuration to read the connection string
            var optionsBuilder = new DbContextOptionsBuilder<SMSDbContext>();

            // Set up configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ensure correct base path
                .AddJsonFile("appsettings.json") // Point to your appsettings.json
                .Build();

            // Get the connection string from the configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configure the DbContext with the connection string
            optionsBuilder.UseSqlServer(connectionString);

            // Return a new instance of the SMSDbContext
            return new SMSDbContext(optionsBuilder.Options);
        }
    }
}

