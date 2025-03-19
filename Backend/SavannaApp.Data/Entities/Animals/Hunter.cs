using SavannaApp.Data.Constants;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Hunter(int id, int x, int y, IMovement movement) : Animal(id, x, y)
    {
        private readonly IMovement _hunter = movement;

        /// <summary>
        /// Method for hunter movement logic
        /// </summary>
        /// <param name="map">Map</param>
        public override void Move(IMap map)
        {
            var action = _hunter.Move(this, map);

            if (!action)
            {
                RandomMovement.Move(this, map);
            }

            DecreaseHealth(GameConstants.HealthDamageOnMove);

            IncreaseAge();
        }
    }
}
