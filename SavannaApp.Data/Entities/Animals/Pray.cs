using SavannaApp.Data.Constants;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Pray(int id, int x, int y, IMovement movement) : Animal(id, x, y)
    {
        private readonly IMovement _pray = movement;

        /// <summary>
        /// Method for pray movement logic
        /// </summary>
        /// <param name="map"></param>
        public override void Move(IMap map)
        {
            var action = _pray.Move(this, map);

            if (!action)
            {
                RandomMovement.Move(this, map);
            }

            DecreaseHealth(GameConstants.HealthDamageOnMove);
        }
    }
}
