using AutoMapper;
using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Dto.Game.Save;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Business.Services
{
    public class DtoMapper(AnimalTypeMapper animalTypeMapper, IAnimalFactory animalFactory) : IDtoMapper
    {
        /// <summary>
        /// Map game to game save dto
        /// </summary>
        /// <param name="game">game</param>
        /// <returns>game save dto</returns>
        public GameSaveDto MapGameSave(Game game)
        {
            var gameSave = new GameSaveDto
            {
                Id = game.Id,
                UserId = game.UserId,
                Iteration = game.Iteration,
                Map = new MapSaveDto
                {
                    Height = game.Map.Height,
                    Width = game.Map.Width,
                }
            };
            foreach (var animal in game.Map.Animals)
            {
                var animalSave = MapAnimalSave(animal);
                if (animalSave != null) gameSave.Map.Animals.Add(animalSave);
            }

            return gameSave;
        }

        /// <summary>
        /// Map game save dto to game
        /// </summary>
        /// <param name="gameSaveDto">game save dto</param>
        /// <returns>game</returns>
        public Game MapGame(GameSaveDto gameSaveDto)
        {
            var game = new Game
            {
                Id = gameSaveDto.Id,
                UserId = gameSaveDto.UserId,
                Iteration = gameSaveDto.Iteration,
                Map = new Map(gameSaveDto.Map.Height, gameSaveDto.Map.Width)
            };

            foreach (var animalSave in gameSaveDto.Map.Animals)
            {
                var animal = MapAnimal(animalSave);
                if(animal != null) game.Map.Animals.Add(animal);
            }

            return game;
        }

        /// <summary>
        /// Map animal save dto to animal
        /// </summary>
        /// <param name="animal">animal save dto</param>
        /// <returns>animal</returns>
        public Animal? MapAnimal(AnimalSaveDto animal)
        {
            var type = animalTypeMapper.GetType(animal.AnimalTypeId);
            if (type == null) return null;

            var mapped = animalFactory.CreateAnimal(type, animal.Position.X, animal.Position.Y);

            mapped.SetFeatures(animal.Features);

            return mapped;
        }

        /// <summary>
        /// Map animal to animal save dto
        /// </summary>
        /// <param name="animal">animal</param>
        /// <returns>animal save dto</returns>
        public AnimalSaveDto? MapAnimalSave(Animal animal)
        {
            var mapped = new AnimalSaveDto
            {
                Position = animal.Position,
                Features = animal.Features,
            };

            var animalTypeId = animalTypeMapper.GetAnimalId(animal.GetType());

            if (animalTypeId != null)
            {
                mapped.AnimalTypeId = (int)animalTypeId;
                return mapped;
            }

            return null;
        }
    }
}
