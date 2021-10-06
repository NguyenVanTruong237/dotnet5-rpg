using System.Threading.Tasks;
using dotnet5_rpg.Data;
using dotnet5_rpg.Dtos.User;
using dotnet5_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet5_rpg.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _auRepo;
        public AuthController(IAuthRepository auRepo)
        {
            _auRepo = auRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register (UserRegisterDto request)
        {
            var serviceResponse = await _auRepo.Register(
                new User {UserName = request.UserName}, request.Password
            );
            if(!serviceResponse.Success)
            {
                return BadRequest(serviceResponse);
            }
            return Ok(serviceResponse);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login (UserLoginDto request)
        {
            var serviceResponse = await _auRepo.Login(request.UserName, request.Password);
            if(!serviceResponse.Success)
            {
                return BadRequest(serviceResponse);
            }
            return Ok(serviceResponse);
        }
    }
}