﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Interfaces
{
    public interface IAnimalCreationService
    {
        public Animal? CreateAnimal(Type animalType, IMap map);
    }
}
