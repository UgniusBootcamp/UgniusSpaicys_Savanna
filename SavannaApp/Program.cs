using GameOfLife.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using SavannaApp.Business.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var services = DependencyContainer.ConfigureService();

        services.GetRequiredService<IGameService>().Execute();
    }
}
