﻿using SavannaApp.Data.Constants;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Animal(int id, int x, int y, string name, AnimalFeatures features, IMovement movement)
    {
        public int Id { get; } = id;
        public string Name { get; } = name;
        public AnimalFeatures Features { get; } = features;
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
            Features.Health = 0;
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
            if (damage < 0) return;

            Features.Health -= damage;

            IsAlive = Features.Health > 0;
        }

        public void IncreaseHealth(double health)
        {
            if (health < 0) return;

            Features.Health += health;
        }
    }
}
