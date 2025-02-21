using SavannaApp.Data.Constants;
using System.Diagnostics.Metrics;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities.Animals
{
    public class Pray : Animal
    {
        private readonly IMovement _pray;
        public Pray(int id, int x, int y, string name, AnimalFeatures features, IMovement movement) : base(id, x, y, name, features)
        {
            _pray = movement;
        }

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
