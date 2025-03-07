using AutoMapper;
using SavannaApp.Data.Dto.Account;
using SavannaApp.Data.Entities.Auth;

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
        }
    }
}
