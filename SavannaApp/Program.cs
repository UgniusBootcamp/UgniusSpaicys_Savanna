using GameOfLife.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using SavannaApp.Data.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var services = DependencyContainer.ConfigureService();

        services.GetRequiredService<IGameService>().Execute();
    }
}
