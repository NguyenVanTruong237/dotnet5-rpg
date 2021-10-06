using System.Threading.Tasks;
using dotnet5_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet5_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var serviceResponse = new ServiceResponse<string>();
            var userInObj = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(username.ToLower()));
            if(userInObj == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User not found.";
            }
            else
                if(VerifyPasswordHash(password, userInObj.PasswordHash, userInObj.PasswordSalt) == false)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Wrong Password";
                }
                else
                {
                    serviceResponse.Data = userInObj.Id.ToString();
                    serviceResponse.Message = "Login success";
                }
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var serviceResponse = new ServiceResponse<int>();
            if(await UserExists(user.UserName))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User already exists";
                return serviceResponse;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            serviceResponse.Data = user.Id;
            return serviceResponse; 
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.UserName.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computerHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < computerHash.Length; i++)
                {
                    if(computerHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}