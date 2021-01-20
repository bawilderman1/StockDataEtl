using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration.Json;
using StockDataEtl.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDataEtl
{
    class Program
    {
        static void Main(string[] args)
        {
            var envVars = Environment.GetEnvironmentVariables();
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // create service collection
            var services = new ServiceCollection();

            // build config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();

            // setup config
            services.AddOptions();
            services.Configure<AppSettings>(configuration.GetSection("App"));

            // create service provider
            var serviceProvider = services.BuildServiceProvider();

            var appSettings = serviceProvider.GetService<IOptions<AppSettings>>();
            var appSettingsValue = appSettings.Value;

            Console.WriteLine();
        }
    }
}
