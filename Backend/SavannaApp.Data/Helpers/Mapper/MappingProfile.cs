using AutoMapper;
using SavannaApp.Data.Dto.Account;
using SavannaApp.Data.Dto.Game;
using SavannaApp.Data.Dto.Game.Save;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Entities.Auth;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Helpers.Mapper
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Mapper
        /// </summary>
        public MappingProfile() 
        {
            //User
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();

            //Map
            CreateMap<IMap, MapReadDto>();

            //Animal
            CreateMap<Animal, AnimalReadDto>()
                .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.Position.X))
                .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.Position.Y))
                .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.GetType().Name));

            //Animal Features
            CreateMap<AnimalFeatures, AnimalFeaturesReadDto>();

            //Game
            CreateMap<Game, GameReadDto>()
                .ForMember(dest => dest.AnimalCount, opt => opt.MapFrom(src => src.Map.Animals.GroupBy(animal => animal.GetType().Name)
                .ToDictionary(group => group.Key, group => group.Count())));

            CreateMap<Game, GameLoadInfoDto>()
                .ForMember(dest => dest.AnimalCount, opt => opt.MapFrom(src => src.Map.Animals.GroupBy(animal => animal.GetType().Name)
                .ToDictionary(group => group.Key, group => group.Count())));
        }
    }
}
