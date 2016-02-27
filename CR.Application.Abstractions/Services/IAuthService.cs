using CR.Application.Abstractions.Models;
using System.Threading.Tasks;

namespace CR.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<RegisterResultViewModel> RegisterUser(RegisterUserViewModel userModel);
        Task<UserViewModel> FindUser(string userName, string password);
        Task<UserViewModel> GetUserInfo(string id);
        Task<UserViewModel> GetUserInfoByUsername(string userName);
    }
}
