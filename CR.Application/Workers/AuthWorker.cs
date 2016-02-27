using CR.Application.Abstractions.Models;
using CR.Application.Abstractions.Services;
using CR.Infrastructure;
using System;
using System.Threading.Tasks;

namespace CR.Application.Workers
{
    public class AuthWorker : IAuthWorker
    {
        private readonly IAuthService auth;

        public AuthWorker(IAuthService auth)
        {
            Contract.Requires<ArgumentNullException>(auth != null, "IAuthService auth");
            this.auth = auth;
        }

        public async Task<UserViewModel> FindUser(string userName, string password)
        {
            return await auth.FindUser(userName, password);
        }

        public async Task<UserViewModel> GetUserInfo(string id)
        {
            return await auth.GetUserInfo(id);
        }

        public async Task<UserViewModel> GetUserInfoByUsername(string userName)
        {
            return await auth.GetUserInfoByUsername(userName);
        }

        public async Task<RegisterResultViewModel> RegisterUser(RegisterUserViewModel userModel)
        {
            return await auth.RegisterUser(userModel);
        }
    }
}