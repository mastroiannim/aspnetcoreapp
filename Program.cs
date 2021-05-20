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
                    var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
                    var PORT = configuration.GetSection("PORT").Value;

                    if(PORT == null)
                    {
                        Console.Write("reading PORT locally.. ");
                        configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                        PORT = configuration.GetConnectionString("PORT");
                    }
                    else
                        Console.WriteLine("reading from Environment *heroku");

                    Console.WriteLine("PORT={0}", PORT);

                    int httpPort = Int32.Parse(PORT);

                    webBuilder.UseUrls("http://*:"+httpPort);
                 
                });
    }
}
