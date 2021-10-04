using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet5_rpg.Models;

namespace dotnet5_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> Characters = new List<Character>{
            new Character(),
            new Character{Id =1, Name="Sam"}
        };
        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character character)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            Characters.Add(character);
            serviceResponse.Data = Characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            serviceResponse.Data = Characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<Character>();
            serviceResponse.Data = Characters.FirstOrDefault(c => c.Id == id);
            return serviceResponse;
        }
    }
}