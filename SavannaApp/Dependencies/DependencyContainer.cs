using Microsoft.Extensions.DependencyInjection;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Interfaces.UI;
using SavannaApp.Business.Services;
using SavannaApp.Data.Helpers.Map;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;
using SavannaApp.UI;

namespace GameOfLife.Dependencies
{
    public static class DependencyContainer
    {
        /// <summary>
        /// Method to create services
        /// </summary>
        /// <returns>Service provider</returns>
        public static ServiceProvider ConfigureService()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IMapPrinter, MapPrinter>();
            services.AddSingleton<IOutputHandler, ConsoleOutput>();
            services.AddSingleton<IInputHandler, ConsoleInput>();
            services.AddSingleton<IAnimalFactory, AnimalFactory>();

            services.AddTransient<IMapCreator, MapCreationService>();
            services.AddTransient<HunterMovement>();
            services.AddTransient<PrayMovement>();

            services.AddScoped<IMapManager, MapManager>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IAnimalCreationService, AnimalCreationService>();
            services.AddScoped<IAnimalGroupManager, AnimalGroupManager>();

            return services.BuildServiceProvider();
        }
    }
}