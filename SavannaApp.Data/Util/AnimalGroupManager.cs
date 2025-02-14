using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Util
{
    public class AnimalGroupManager(IAnimalCreationService animalCreationService) : IAnimalGroupManager
    {
        private readonly int rebirthRadius = 1;
        private readonly int rebirthCounter = 3;

        private Dictionary<(int, int), AnimalGroup> AnimalGroups = new Dictionary<(int, int), AnimalGroup>();
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

                    if (rebirth != null) map.SetAnimal(rebirth);

                    AnimalGroups.Remove(kvp.Key);
                }
            }
        }

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

        private void ValidateGroupsExistance()
        {
            foreach (var kvp in AnimalGroups)
            {
                if (!kvp.Value.GroupStillExist(rebirthRadius)) AnimalGroups.Remove(kvp.Key);
            }
        }
    }
}
