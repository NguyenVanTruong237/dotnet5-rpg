using AutoMapper;
using dotnet5_rpg.Dtos.Character;
using dotnet5_rpg.Dtos.Skill;
using dotnet5_rpg.Dtos.Weapon;
using dotnet5_rpg.Models;

namespace dotnet5_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character,GetCharacterDto>();
            CreateMap<AddCharacterDto,Character>();
            CreateMap<Weapon,GetWeaponDto>();
            CreateMap<Skill,GetSkillDto>();
        }
    }
}