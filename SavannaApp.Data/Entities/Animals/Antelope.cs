using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Entities.Animals
{
    public class Antelope : Animal
    {
        public Antelope(int id, int x, int y, string name, int speed, int vision, IMovement movement) : base(id, x, y, name, speed, vision, movement)
        {
        }
    }
}
