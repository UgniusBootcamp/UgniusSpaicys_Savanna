using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities.Animals
{
    public class Antelope(int id, int x, int y, string name, int speed, int vision, double health, IMovement movement) : Animal(id, x, y, name, speed, vision, health, movement)
    {
    }
}
