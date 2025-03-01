﻿using System.Text.Json;
using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Helpers.Configuration;

namespace SavannaApp.Business.Services
{
    public class JsonAnimalConfigurationReader : IAnimalConfigReader
    {
        private string _baseDir = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Method to create animal
        /// </summary>
        /// <param name="animalType">Type of animal</param>
        /// <param name="map">Map</param>
        /// <returns>Created animal or null if there is no free space in map</returns>
        public IEnumerable<AnimalConfig> GetAnimalConfiguration(string fileName)
        {
            try
            {
                fileName = String.Format("{0}{1}", _baseDir, fileName);

                if (!File.Exists(fileName)) throw new FileNotFoundException();

                string json = File.ReadAllText(fileName);
                var result = JsonSerializer.Deserialize<List<AnimalConfig>>(json);

                return result ?? [];
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case FileNotFoundException _:
                        throw new FileNotFoundException(String.Format(GameConstants.FileNotFound, fileName));
                    default:
                        throw new Exception(String.Format(GameConstants.ConfigLoadFail, e.Message));
                }
            }
        }
    }
}
