using Moq;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Tests.Helpers
{
    public static class AnimalMock
    {
        public static Mock<IMovement> Movement = new Mock<IMovement>();

        public static Lion CreateLion(int id = 1, int x = 0, int y = 0, string name = "L", int speed = 3, int vision = 3, double health = 50, IMovement? movement = null)
        {
            var movementMock = movement ?? Movement.Object;

            return new Lion(id, x, y, name, new AnimalFeatures(speed, vision, health), movementMock);
        }

        public static Antelope CreateAntelope(int id = 1, int x = 0, int y = 0, string name = "L", int speed = 3, int vision = 3, double health = 50, IMovement? movement = null)
        {
            var movementMock = movement ?? Movement.Object;

            return new Antelope(id, x, y, name, new AnimalFeatures(speed, vision, health), movementMock);
        }
    }
}
