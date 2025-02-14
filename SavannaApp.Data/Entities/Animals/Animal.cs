using SavannaApp.Data.Constants;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Animal(int id, int x, int y, string name, int speed, int vision, double health, IMovement movement)
    {
        public int Id { get; } = id;
        public string Name { get; } = name;
        public int Speed { get; } = speed;
        public int Vision { get; } = vision;
        public double Health { get; private set; } = health;
        public bool IsAlive { get; private set; } = true;
        public Position Position { get; } = new Position(x, y);
        private IMovement _movement { get; set; } = movement;

        /// <summary>
        /// Method to calculate distance to position (Manhattan)
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>Distance to position</returns>
        public int DistanceTo(int x, int y)
        {
            return Math.Max(Math.Abs(Position.X - x), Math.Abs(Position.Y - y));
        }

        /// <summary>
        /// Method to make animal dead
        /// </summary>
        public void Death()
        {
            IsAlive = false;
        }

        /// <summary>
        /// Method for performing movement of animal
        /// </summary>
        /// <param name="map">Map</param>
        public void Move(IMap map)
        {
            var action = _movement.Move(this, map);

            if (!action)
            {
                var random = new RandomMovement();
                random.Move(this, map);
            }

            DecreaseHealth(GameConstants.HealthDamageOnMove);
        }

        private void DecreaseHealth(double damage)
        {
            Health -= damage;

            if (Health <= 0) IsAlive = false;
        }

        public void IncreaseHealth(double health)
        {
            Health += health;
        }
    }
}
