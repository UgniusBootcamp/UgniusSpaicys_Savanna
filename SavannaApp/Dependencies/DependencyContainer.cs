using GameOfLife.Data.Interfaces.Game;
using GameOfLife.Data.Interfaces.UI;
using GameOfLife.Data.Util;
using GameOfLife.UI;
using Microsoft.Extensions.DependencyInjection;
using SavannaApp.Data.Factory;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;
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
            services.AddTransient<IAnimalCreationService, AnimalCreationService>();

            services.AddScoped<IGameService, GameService>();

            return services.BuildServiceProvider();
        }
    }
}