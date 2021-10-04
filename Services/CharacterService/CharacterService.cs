using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet5_rpg.Dtos.Character;
using dotnet5_rpg.Models;

namespace dotnet5_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> Characters = new List<Character>{
            new Character(),
            new Character{Id =1, Name="Sam"}
        };
        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(Characters.FirstOrDefault(c => c.Id ==id));
            return serviceResponse;
        }
         public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var obj = _mapper.Map<Character>(character);
            obj.Id = Characters.Max(c => c.Id)+1;
            Characters.Add(obj);
            serviceResponse.Data = Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }
    }
}