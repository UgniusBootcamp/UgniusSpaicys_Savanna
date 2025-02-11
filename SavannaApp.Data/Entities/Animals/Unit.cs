namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Unit(int id, int x, int y)
    {
        public int Id { get; set; } = id;
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
    }
}
