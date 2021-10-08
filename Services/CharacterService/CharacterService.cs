using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet5_rpg.Dtos.Character;
using dotnet5_rpg.Models;
using System;
using dotnet5_rpg.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace dotnet5_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterService(IMapper mapper
        , DataContext context
        , IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
                    .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.Where(c => c.User.Id == GetUserId()).ToListAsync();
            if (dbCharacters.Count != 0)
            {
                serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters
            .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            if (dbCharacter != null)
            {
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character characterNew = _mapper.Map<Character>(character);
            characterNew.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            await _context.Characters.AddAsync(characterNew);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters
            .Where(c => c.User.Id == GetUserId())
            .Select(c => _mapper.Map<GetCharacterDto>(c))
            .ToListAsync();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var obj = await _context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                
                if (obj.User.Id == GetUserId())
                {
                    obj.Name = updateCharacter.Name;
                    obj.HitPoints = updateCharacter.HitPoints;
                    obj.Strength = updateCharacter.Strength;
                    obj.Defense = updateCharacter.Defense;
                    obj.Intelligence = updateCharacter.Intelligence;
                    obj.Class = obj.Class;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(obj);
                }
                else
                {
                    serviceResponse.Success= false;
                    serviceResponse.Message = "Character not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var obj = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if (obj != null)
                {
                    _context.Remove(obj);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Characters
                    .Where(c => c.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}