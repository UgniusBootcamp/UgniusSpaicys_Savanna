using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Services
{
    public class AnimalGroupManager(IAnimalCreationService animalCreationService) : IAnimalGroupManager
    {
        private readonly int rebirthRadius = GameConstants.DefaultRebirthRadius;
        private readonly int rebirthCounter = GameConstants.DefaultRebirthCounter;

        private Dictionary<(int, int), AnimalGroup> AnimalGroups = new Dictionary<(int, int), AnimalGroup>();
        
        /// <summary>
        /// Method for animal reproduction
        /// </summary>
        /// <param name="map">map</param>
        public void Reproduction(IMap map)
        {
            var Animals = map.Animals;

            ValidateGroupsExistance();

            FormGroups(Animals);

            List<Animal> rebirthAnimals = new List<Animal>();

            foreach (var kvp in AnimalGroups) 
            {
                if (kvp.Value.RoundsTogether == rebirthCounter)
                {
                    var rebirth = animalCreationService.RebirthAnimal(kvp.Value.Animals, map);

                    if (rebirth != null) 
                    {
                        map.SetAnimal(rebirth);
                        kvp.Value.IncreaseOffSprings();
                    }

                    AnimalGroups.Remove(kvp.Key);
                }
            }
        }

        /// <summary>
        /// Method to form animal group
        /// </summary>
        /// <param name="Animals">animals</param>
        private void FormGroups(List<Animal> Animals)
        {
            int n = Animals.Count();

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    var a1 = Animals[i];
                    var a2 = Animals[j];

                    if (a1.GetType().Equals(a2.GetType())) CheckGroup(a1, a2);
                }
            }
        }

        /// <summary>
        /// Method to add new group or increase existing counter
        /// </summary>
        /// <param name="a1">animal</param>
        /// <param name="a2">animal</param>
        private void CheckGroup(Animal a1, Animal a2)
        {
            if (a1.DistanceTo(a2.Position.X, a2.Position.Y) <= rebirthRadius)
            {
                var key = (Math.Min(a1.Id, a2.Id), Math.Max(a1.Id, a2.Id));

                if (!AnimalGroups.ContainsKey(key))
                {
                    var group = new AnimalGroup(a1, a2);

                    group.RoundTogether();

                    AnimalGroups[key] = group;
                }
                else
                {
                    var group = AnimalGroups[key];

                    group.RoundTogether();
                }
            }
        }

        /// <summary>
        /// Method for group existance validation
        /// </summary>
        private void ValidateGroupsExistance()
        {
            foreach (var kvp in AnimalGroups)
            {
                if (!kvp.Value.GroupStillExist(rebirthRadius)) AnimalGroups.Remove(kvp.Key);
            }
        }
    }
}
