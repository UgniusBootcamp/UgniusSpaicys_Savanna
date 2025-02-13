﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Interfaces.Game
{
    public interface IMap
    {
        int Height { get; }
        int Width { get; }

        public List<Animal> Animals { get; }

        public void SetAnimal(Animal animal);

        public Animal? GetAnimal(int x, int y);

        public bool IsPositionValid(int x, int y);

        public void RemoveAnimal(Animal animal);
    }
}
