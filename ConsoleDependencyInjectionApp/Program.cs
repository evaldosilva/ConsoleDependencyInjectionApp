using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Reflection;

namespace ConsoleDependencyInjectionApp
{
    class Program
    {
        static int Main()
        {
            var configuration = GetConfiguration();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration) // Read from a config file (serilog.json or inside appsettings.json)
                .WriteTo.Console() // Or use a pre-built sync config
                .CreateLogger();
            try
            {
                Log.Logger.Information("Starting...");

                var provider = StartServiceCollection(configuration);
                var service = provider.GetRequiredService<IMyConsoleService>();
                service.WriteToConsoleSomething();

                Log.Logger.Information("...Finishing.");

                return 0;
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex, "Ooops...");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static ServiceProvider StartServiceCollection(IConfiguration configuration)
        {
            return new ServiceCollection()
                .AddLogging(builder => builder.AddSerilog())
                .RegisterMyDependencies(configuration) // Here is where we start our Dependency Injection, loading classes from our custom code (extension method)
                .BuildServiceProvider();
        }

        private static IConfiguration GetConfiguration()
        {
            ConfigurationBuilder builder = new();

            // Read configuration file, as optional
            builder.AddJsonFile("appsettings.json", optional: true);

            // Here we can read from appsettings.json or secrets.json, depending on the environment
            if (IsDevelopment())
                builder.AddUserSecrets(Assembly.GetExecutingAssembly());

            return builder.Build();
        }

        private static bool IsDevelopment()
        {
            return Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "DEV";
        }
    }
}