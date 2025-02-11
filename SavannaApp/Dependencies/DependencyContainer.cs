using Microsoft.Extensions.DependencyInjection;
using SavannaApp.Data.Factory;
using SavannaApp.Data.Interfaces;
using SavannaApp.UI;


namespace GameOfLife.Dependencies
{
    public static class DependencyContainer
    {
        public static ServiceProvider ConfigureService()
        {
            var services = new ServiceCollection();

            services.AddTransient<AnimalFactory, LionFactory>();
            services.AddTransient<AnimalFactory, AntelopeFactory>();
            services.AddSingleton<IMapPrinter, MapPrinter>();

            return services.BuildServiceProvider();
        }
    }
}