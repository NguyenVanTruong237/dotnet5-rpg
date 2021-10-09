using System.Collections.Generic;
using dotnet5_rpg.Dtos.Skill;
using dotnet5_rpg.Dtos.Weapon;
using dotnet5_rpg.Models;

namespace dotnet5_rpg.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; }  =10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public GetWeaponDto weapon { get; set; }
        public List<GetSkillDto> Skills {get; set;}
    }
}