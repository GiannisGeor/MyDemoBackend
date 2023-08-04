using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Read config
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true, reloadOnChange: true)
                .Build();

            // Build logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(
                    connectionString: config.GetConnectionString("Log"),
                    sinkOptions: new MSSqlServerSinkOptions()
                    {
                        AutoCreateSqlTable = true,
                        SchemaName = "Diagnostics",
                        TableName = "Logs"
                    })
                .CreateLogger();

            try
            {
                Log.Information("App started");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Failed to launch App");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}