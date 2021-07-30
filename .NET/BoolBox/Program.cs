using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BoolBox.Data;
using BoolBox.Models;

using IronPython.Hosting;

namespace BoolBox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter a string to print from Python");
            //var input = Console.ReadLine();
            var py = Python.CreateEngine();
            try
            {
                py.ExecuteFile("C:\\Users\\Jonald\\Documents\\bool-box\\Python\\main.py");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            //Console.WriteLine("Press Enter to Exit");
            //Console.ReadLine();


            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
