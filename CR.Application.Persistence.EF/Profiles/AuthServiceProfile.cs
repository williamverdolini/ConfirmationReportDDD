using AutoMapper;
using CR.Application.Abstractions.Models;
using CR.Application.Persistence.EF.Models;
using Microsoft.AspNet.Identity;

namespace CR.Application.Persistence.EF.Profiles
{
    public class AuthServiceProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<IdentityResult, RegisterResultViewModel>();            
        }
    }
}
