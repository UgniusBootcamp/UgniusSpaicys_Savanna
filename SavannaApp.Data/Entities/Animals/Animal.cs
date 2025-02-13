using SavannaApp.Data.Entities.MovementStrategies;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Animal(int id, int x, int y, string name, int speed, int vision, IMovement movement)
    {
        public int Id { get; } = id;
        public string Name { get; } = name;
        public int Speed { get; } = speed;
        public int Vision { get; } = vision;
        public bool IsAlive { get; private set; } = true;
        public Position Position { get; } = new Position(x, y);
        private IMovement _movement { get; set; } = movement;

        public double DistanceTo(int x, int y)
        {
            return Math.Max(Math.Abs(Position.X - x), Math.Abs(Position.Y - y));
        }

        public void Death()
        {
            IsAlive = false;
        }

        public void Move(IMap map)
        {
            var action = _movement.Move(this, map);

            if (!action)
            {
                var random = new RandomMovement();
                random.Move(this, map);
            }
        }
    }
}
