using System.Collections.Generic;
using System.Linq;
using dotnet5_rpg.Models;

namespace dotnet5_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> Characters = new List<Character>{
            new Character(),
            new Character{Id =1, Name="Sam"}
        };
        public List<Character> AddCharacter(Character character)
        {
            Characters.Add(character);
            return Characters;
        }

        public List<Character> GetAllCharacters()
        {
            return Characters;
        }

        public Character GetCharacterById(int id)
        {
            return Characters.FirstOrDefault(c => c.Id ==id);
        }
    }
}