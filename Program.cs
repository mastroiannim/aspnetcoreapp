using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace aspnetcoreapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("START");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    var configuration = new ConfigurationBuilder()
                            .AddEnvironmentVariables()
                            .Build();
                    var PORT = configuration.GetSection("PORT").Value;
                    Console.WriteLine("PORT from Env Var to convert '{0}'.", PORT);
                    int httpPort;
                    try
                    {
                        httpPort = Int32.Parse(PORT);
                    }
                    catch (System.Exception)
                    {
                        Console.WriteLine("Unable to convert '{0}' using 5000.", PORT);
                        httpPort = 5000;
                    }
                    webBuilder.UseUrls("http://*:"+httpPort);
                 
                });
    }
}
