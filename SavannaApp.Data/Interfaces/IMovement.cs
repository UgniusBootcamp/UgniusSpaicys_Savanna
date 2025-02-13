using System;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Interfaces
{
    public interface IMovement
    {
        bool Move(Animal animal, IMap map);
    }
}
