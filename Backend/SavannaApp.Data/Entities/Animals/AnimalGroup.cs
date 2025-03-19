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

        /// <summary>
        /// Method to increase round together counter
        /// </summary>
        public void RoundTogether() 
        {
            RoundsTogether++;
        }

        /// <summary>
        /// Method to check whether group still eligible to exist
        /// </summary>
        /// <param name="range">valid which animals have to be within</param>
        /// <returns>true if both alive and in range, false otherwise</returns>
        public bool GroupStillExist(int range)
        {
            var a1 = Animals.First(); var a2 = Animals.Last();

            if (a1.DistanceTo(a2.Position.X, a2.Position.Y) > range) return false;

            if (Animals.Any(a => !a.IsAlive)) return false;

            return true;
        }

        public void IncreaseOffSprings()
        {
            foreach (var a in Animals) a.IncreaseOffSprings();
        }
    }
}
