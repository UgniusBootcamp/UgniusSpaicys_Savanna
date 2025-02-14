namespace SavannaApp.Data.Entities.Animals
{
    public class AnimalGroup
    {
        public int RoundsTogether { get; private set; } = 0;

        public List<Animal> Animals = new List<Animal>();

        public AnimalGroup(Animal a1, Animal a2)
        {
            Animals.Add(a1);
            Animals.Add(a2);
        }

        public void RoundTogether() 
        {
            RoundsTogether++;
        }

        public bool GroupStillExist(int range)
        {
            var a1 = Animals.First(); var a2 = Animals.Last();

            if (a1.DistanceTo(a2.Position.X, a2.Position.Y) > range) return false;

            if (Animals.Any(a => !a.IsAlive)) return false;

            return true;
        }
    }
}
