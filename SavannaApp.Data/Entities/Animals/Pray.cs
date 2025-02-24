using SavannaApp.Data.Constants;
using System.Diagnostics.Metrics;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Helpers.Map;

namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Pray(int id, int x, int y, IMovement movement) : Animal(id, x, y)
    {
        private readonly IMovement _pray = movement;

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
