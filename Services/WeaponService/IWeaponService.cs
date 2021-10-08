using System.Threading.Tasks;
using dotnet5_rpg.Dtos.Character;
using dotnet5_rpg.Dtos.Weapon;
using dotnet5_rpg.Models;

namespace dotnet5_rpg.Services.WeaponService
{
    public interface IWeaponService
    {
         Task<ServiceResponse<GetCharacterDto>> AddWeapon (AddWeaponDto newWeapon);
    }
}