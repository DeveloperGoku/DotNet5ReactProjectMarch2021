using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {
           // CreateHostBuilder(args).Build().Run();
           var host = CreateHostBuilder(args).Build();
           using var scope = host.Services.CreateScope(); // disposed of by the framework and will not be left hanging around.
           var services = scope.ServiceProvider;
           try
           {
               var context = services.GetRequiredService<DataContext>(); // service locator pattern used.
                context.Database.Migrate();
                await Seed.SeedData(context);
           }
           catch(Exception ex)
           {
                var logger = services.GetRequiredService<ILogger<Program>>();  
                logger.LogError(ex, "An error occured during migration");
           }
            host.Run();  // Don't forget to add this line as this will run the app.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
