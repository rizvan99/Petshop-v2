using Microsoft.Extensions.DependencyInjection;
using Petshop2020.Core.Application_Service;
using Petshop2020.Core.Application_Service.Service;
using Petshop2020.Core.Domain_Service;
using Petshop2020.Infrastructure.Data;
using System;

namespace Petshop2020.UI
{
    class Program
    {
        static void Main(string[] args)
        {


            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IPrinter, Printer>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var printer = serviceProvider.GetRequiredService<IPrinter>();

            printer.StartUI();

        }
    }
}
