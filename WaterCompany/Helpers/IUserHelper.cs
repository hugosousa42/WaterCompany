using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WaterCompany.Data.Entities;
using WaterCompany.Models;

namespace WaterCompany.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    }
}
