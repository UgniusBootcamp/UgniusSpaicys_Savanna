namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Animal : Unit
    {
        public string Name { get; private set; }
        public int Speed { get; private set; }
        public int Vision { get; private set; }
        protected Animal(int id, int x, int y, string name, int speed, int vision) : base(id, x, y)
        {
            Speed = speed;
            Vision = vision;
            Name = name;
        }
    }
}
