using System.Threading.Tasks;
using dotnet5_rpg.Dtos.Fight;
using dotnet5_rpg.Models;
using dotnet5_rpg.Services.FightService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet5_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightservice;
        public FightController(IFightService fightService)
        {
            _fightservice = fightService;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack (WeaponAttackDto request)
        {
            return Ok(await _fightservice.WeaponAttack(request));
        }
    }
}