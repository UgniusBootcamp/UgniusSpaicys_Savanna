﻿using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities.Animals
{
    public class Lion(int id, int x, int y, string name, AnimalFeatures features, IMovement movement) : Animal(id, x, y, name, features, movement)
    {
    }
}
