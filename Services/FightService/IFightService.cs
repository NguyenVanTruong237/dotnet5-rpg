using System.Threading.Tasks;
using dotnet5_rpg.Dtos.Fight;
using dotnet5_rpg.Models;

namespace dotnet5_rpg.Services.FightService
{
    public interface IFightService
    {
         Task<ServiceResponse<AttackResultDto>> WeaponAttack (WeaponAttackDto request);
    }
}