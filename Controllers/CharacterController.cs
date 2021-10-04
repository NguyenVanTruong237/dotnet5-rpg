using System.Collections.Generic;
using System.Linq;
using dotnet5_rpg.Models;
using dotnet5_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet5_rpg.Controllers
{
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
        public ActionResult<List<Character>> Get()
        {
            return Ok(_CharacterService.GetAllCharacters());
        }
        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(_CharacterService.GetCharacterById(id));
        }
        [HttpPost]
        public ActionResult<List<Character>> Post (Character character)
        {
            return _CharacterService.AddCharacter(character);
        }
    }
}