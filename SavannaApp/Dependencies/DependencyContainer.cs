using GameOfLife.Data.Interfaces.Game;
using GameOfLife.Data.Interfaces.UI;
using GameOfLife.Data.Util;
using GameOfLife.UI;
using Microsoft.Extensions.DependencyInjection;
using SavannaApp.Data.Entities.MovementStrategies;
using SavannaApp.Data.Factory;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;
using SavannaApp.Data.Interfaces.Map;
using SavannaApp.Data.Util;
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