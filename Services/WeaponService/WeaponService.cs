using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet5_rpg.Data;
using dotnet5_rpg.Dtos.Character;
using dotnet5_rpg.Dtos.Weapon;
using dotnet5_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet5_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        
        public WeaponService(DataContext context
        ,IHttpContextAccessor httpContextAccessor
        ,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
                    .FindFirstValue(ClaimTypes.NameIdentifier));
                    
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var characterInDb = await _context.Characters.FirstOrDefaultAsync(
                    c => c.Id == newWeapon.CharacterId && c.User.Id == GetUserId());
                if(characterInDb == null)
                {
                    ServiceResponse.Success = false;
                    ServiceResponse.Message = "Character not found.";
                }
                else
                {
                    var weapon = new Weapon
                    {
                        Name = newWeapon.Name,
                        Damage = newWeapon.Damage,
                        Character = characterInDb
                    };
                    await _context.Weapons.AddAsync(weapon);
                    await _context.SaveChangesAsync();

                    ServiceResponse.Data = _mapper.Map<GetCharacterDto>(characterInDb);
                }
            }
            catch(Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
    }
}