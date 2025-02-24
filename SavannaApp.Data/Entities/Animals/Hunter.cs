using SavannaApp.Data.Constants;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Hunter : Animal
    {
        private readonly IMovement _hunter;

        protected Hunter(int id, int x, int y, string name, AnimalFeatures features, IMovement movement) : base(id, x, y, name, features)
        {
            _hunter = movement;
        }

        public override void Move(IMap map)
        {
            var action = _hunter.Move(this, map);

            if (!action)
            {
                RandomMovement.Move(this, map);
            }

            DecreaseHealth(GameConstants.HealthDamageOnMove);
        }
    }
}
