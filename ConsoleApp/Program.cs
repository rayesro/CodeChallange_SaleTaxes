using Application.Interface.Services;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.Memory.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);

            await hostBuilder.RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                {
                    //Scans assemblies and adds handlers, preprocessors, and postprocessors implementations to the container.
                    var assembly = System.AppDomain.CurrentDomain.Load("Application");
                    services.AddMediatR(assembly);

                    services.AddMediatR(Assembly.GetExecutingAssembly());

                    services.AddSingleton<IHostedService, ConsoleApp>();
                    services.AddTransient<IInputHandlerService<string>, ConsoleInputHandlerService>();
                    services.AddTransient<ITaxingService, TaxingService>();
                    services.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();
                    services.AddSingleton<IReceiptPrintingService<string>, ReceiptConsolePrintingService>();
                    services.AddTransient<Ticketizonator>();
                });
    }
}
