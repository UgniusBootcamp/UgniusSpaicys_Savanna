using SavannaApp.Data.Constants;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Animal(int id, int x, int y) : ICreatable
    {
        public int Id { get; } = id;
        public bool IsAlive { get; private set; } = true;
        public Position Position { get; } = new Position(x, y);
        protected readonly IMovement RandomMovement = new RandomMovement();

        public abstract string Name { get; }
        public abstract AnimalFeatures Features { get; }
        public abstract ConsoleKey CreationKey { get; }


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
            Features.Health = 0;
        }

        /// <summary>
        /// Method for performing movement of animal
        /// </summary>
        /// <param name="map">Map</param>
        public virtual void Move(IMap map)
        {
            RandomMovement.Move(this, map);

            DecreaseHealth(GameConstants.HealthDamageOnMove);

            IncreaseAge();
        }

        /// <summary>
        /// Method for decreasing health
        /// </summary>
        /// <param name="damage">helth to decrease</param>
        protected void DecreaseHealth(double damage)
        {
            if (damage < 0) return;

            Features.Health -= damage;

            IsAlive = Features.Health > 0;
        }

        /// <summary>
        /// Method for increasing health
        /// </summary>
        /// <param name="health">health to increase</param>
        public void IncreaseHealth(double health)
        {
            if (health < 0) return;

            Features.Health += health;
        }

        private void IncreaseAge()
        {
            if (!IsAlive) return;

            Features.Age++;
        }

        public void IncreaseOffSprings()
        {
            if (!IsAlive) return;

            Features.Offsprings++;
        }
    }
}
