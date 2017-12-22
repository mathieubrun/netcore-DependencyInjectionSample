using System;
using System.IO;
using Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace App
{
    class Program
    {
        static void Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddOptions();

            var config = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "log.txt"))
                .MinimumLevel.Debug();

            Log.Logger = config.CreateLogger();

            services.AddLogging(log => log.AddSerilog(dispose: true));

            services.Configure<SampleSettings>(configuration);
            services.AddTransient<SampleClass>();

            using (var p = services.BuildServiceProvider())
            {
                var c = p.GetService<SampleClass>();

                c.Go();
            }

            Console.WriteLine("press enter to exit");
            Console.ReadLine();
        }
    }
}
