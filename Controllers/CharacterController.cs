using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet5_rpg.Dtos.Character;
using dotnet5_rpg.Models;
using dotnet5_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet5_rpg.Controllers
{
    [Authorize(Roles = "Player, Admin")]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
       private readonly ICharacterService _CharacterService;
       public CharacterController(ICharacterService characterService)
       {
           _CharacterService = characterService;
       }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            var serviceResponse = await _CharacterService.GetAllCharacters();
            if(serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            var serviceResponse = await _CharacterService.GetCharacterById(id);
            if(serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter (AddCharacterDto character)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _CharacterService.AddCharacter(character));
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter (UpdateCharacterDto updateCharacter)
        {
            var serviceResponse =await _CharacterService.UpdateCharacter(updateCharacter);
            if(serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter (int id)
        {
            var serviceResponse = await _CharacterService.DeleteCharacter(id);
            if(serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }
        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterSkill (AddCharacterSkillDto addCharacterSkill)
        {
            return Ok(await _CharacterService.AddCharacterSkill(addCharacterSkill));
        }
    }
}