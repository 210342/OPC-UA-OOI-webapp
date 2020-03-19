using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ReactiveHMI.ReferenceWebApplication
{
#pragma warning disable CA1052
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.File(
                        @$"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/Log/Serilog/OOI {DateTime.Now.ToShortDateString()}.log",
                        encoding: Encoding.UTF8)
                );
    }
#pragma warning restore CA1052
}
