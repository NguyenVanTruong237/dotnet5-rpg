using System.Threading.Tasks;
using dotnet5_rpg.Models;

namespace dotnet5_rpg.Data
{
    public interface IAuthRepository
    {
         Task<ServiceResponse<int>> Register (User user, string password);
         Task<ServiceResponse<string>> Login(string username, string password);
         Task<bool> UserExists (string username);
    }
}