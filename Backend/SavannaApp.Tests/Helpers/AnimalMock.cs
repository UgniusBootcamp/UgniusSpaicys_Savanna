using SavannaApp.Animals;
using SavannaApp.Data.Helpers.Configuration;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Tests.Helpers
{
    public static class AnimalMock
    {

        private static readonly AnimalConfig lionConfig = new AnimalConfig { Class = "Lion", Health = 50, Key = 'L', Name = "L", Speed = 3, Vision = 6 };
        private static readonly AnimalConfig antelopeConfig = new AnimalConfig { Class = "Antelope", Health = 30, Key = 'A', Name = "A", Speed = 6, Vision = 3 };

        public static Lion CreateLion(int id = 1, int x = 0, int y = 0, IMovement? movement = null)
        {
            var movementMock = movement ?? MovementMock.Movement.Object;

            return new Lion(id, x, y, movementMock, lionConfig);
        }

        public static Antelope CreateAntelope(int id = 1, int x = 0, int y = 0, IMovement? movement = null)
        {
            var movementMock = movement ?? MovementMock.Movement.Object;

            return new Antelope(id, x, y, movementMock, antelopeConfig);
        }
    }
}
