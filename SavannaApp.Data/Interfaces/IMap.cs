﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Interfaces
{
    public interface IMap
    {
        int Height { get; }
        int Width { get; }
        Animal[,] _map { get; }

        public void SetAnimal(Animal animal);

        public Animal GetAnimal(int x, int y);
    }
}
