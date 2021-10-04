using AutoMapper;
using dotnet5_rpg.Dtos.Character;
using dotnet5_rpg.Models;

namespace dotnet5_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character,GetCharacterDto>();
            CreateMap<AddCharacterDto,Character>();
        }
    }
}