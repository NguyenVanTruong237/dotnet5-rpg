using System.Collections.Generic;
using dotnet5_rpg.Models;

namespace dotnet5_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        List<Character> GetAllCharacters();
        Character GetCharacterById (int id);
        List<Character> AddCharacter (Character character);
    }
}