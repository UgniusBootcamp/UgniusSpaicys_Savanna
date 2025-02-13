using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces.Game;
using SavannaApp.Data.Interfaces;

public class RandomMovement : IMovement
{
    private readonly Random _random = new Random();

    public bool Move(Animal animal, IMap map)
    {
        int deltaX = _random.Next(-animal.Speed, animal.Speed + 1);
        int deltaY = _random.Next(-animal.Speed, animal.Speed + 1);

        int maxX = map.Width - 1;
        int maxY = map.Height - 1;

        int newX = animal.Position.X + deltaX;
        int newY = animal.Position.Y + deltaY;

        newX = Math.Clamp(newX, 1, maxX - 1);
        newY = Math.Clamp(newY, 1, maxY - 1);

        if (map.IsPositionValid(newX, newY))
        {
            animal.Position.X = newX;
            animal.Position.Y = newY;
        }

        return true;
    }
}
