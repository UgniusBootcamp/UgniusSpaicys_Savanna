using SavannaApp.Animals;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Tests.Helpers
{
    public static class AnimalMock
    {

        public static Lion CreateLion(int id = 1, int x = 0, int y = 0, IMovement? movement = null)
        {
            var movementMock = movement ?? MovementMock.Movement.Object;

            return new Lion(id, x, y, movementMock);
        }

        public static Antelope CreateAntelope(int id = 1, int x = 0, int y = 0, IMovement? movement = null)
        {
            var movementMock = movement ?? MovementMock.Movement.Object;

            return new Antelope(id, x, y, movementMock);
        }
    }
}
