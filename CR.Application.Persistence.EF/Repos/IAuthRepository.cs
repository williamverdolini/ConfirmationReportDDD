using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using CR.Application.Abstractions.Models;
using CR.Application.Persistence.EF.Models;

namespace CR.Application.Persistence.EF.Repos
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUser(RegisterUserViewModel userModel);        
        Task<ApplicationUser> FindUser(string userName, string password);
        Task<ApplicationUser> GetUserInfo(string id);
        Task<ApplicationUser> GetUserInfoByUsername(string userName);
    }
}
