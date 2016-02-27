using CR.Application.Abstractions.Models;
using CR.Application.Abstractions.Services;
using CR.Application.Persistence.EF.Models;
using CR.Application.Persistence.EF.Repos;
using CR.Infrastructure;
using CR.Infrastructure.Mappings;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace CR.Application.Persistence.EF.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository repo;
        private readonly IMapper mapper;

        public AuthService(IAuthRepository repo, IMapper mapper)
        {
            Contract.Requires<ArgumentNullException>(repo != null, "IAuthRepository repo");
            Contract.Requires<ArgumentNullException>(mapper != null, "IMapper mapper");
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<UserViewModel> FindUser(string userName, string password)
        {
            ApplicationUser user = await repo.FindUser(userName, password);
            return mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> GetUserInfo(string id)
        {
            ApplicationUser user = await repo.GetUserInfo(id);
            return mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> GetUserInfoByUsername(string userName)
        {
            ApplicationUser user = await repo.GetUserInfoByUsername(userName);
            return mapper.Map<UserViewModel>(user);
        }

        public async Task<RegisterResultViewModel> RegisterUser(RegisterUserViewModel userModel)
        {
            IdentityResult result = await repo.RegisterUser(userModel);
            return mapper.Map<RegisterResultViewModel>(result);
        }
    }
}
