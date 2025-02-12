using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Entities.Animals
{
    public class Lion : Animal
    {
        public Lion(int id, int x, int y, string name, int speed, int vision) : base(id, x, y, name, speed, vision)
        {
        }

        public override void NextPosition(IMap map)
        {
            var animal = FindClosestAnimal(typeof(Antelope), map);

            if (animal == null) return;

            CalculateNextPosition(animal);
        }

        private void CalculateNextPosition(Animal animal)
        {
            int moveX = Position.X == animal.Position.X ? 0 : (Position.X < animal.Position.X ? 1 : -1);
            int moveY = Position.Y == animal.Position.Y ? 0 : (Position.Y < animal.Position.Y ? 1 : -1);

            SetPosition(moveX, moveY);
        }
    }
}
