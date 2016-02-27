using CR.Application.Abstractions.Models;
using System.Threading.Tasks;

namespace CR.Application.Workers
{
    public interface IAuthWorker
    {
        Task<RegisterResultViewModel> RegisterUser(RegisterUserViewModel userModel);
        Task<UserViewModel> FindUser(string userName, string password);
        Task<UserViewModel> GetUserInfo(string id);
        Task<UserViewModel> GetUserInfoByUsername(string userName);
    }
}
