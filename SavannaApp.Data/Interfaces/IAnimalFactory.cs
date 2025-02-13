using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Interfaces
{
    public interface IAnimalFactory
    {
        Animal CreateAnimal(Type animalType, int x, int y);
    }
}
