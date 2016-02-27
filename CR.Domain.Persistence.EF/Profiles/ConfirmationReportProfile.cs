using AutoMapper;
using CR.Application.Abstractions.Models;
using CR.Application.Abstractions.Services;
using CR.Domain.Persistence.EF.Models;
using CR.Infrastructure;
using System;
using System.Threading.Tasks;

namespace CR.Domain.Persistence.EF.Profiles
{
    public class ConfirmationReportProfile : Profile
    {
        private readonly IAuthService auth;

        public ConfirmationReportProfile(IAuthService auth)
        {
            Contract.Requires<ArgumentNullException>(auth != null, "IAuthService auth");
            this.auth = auth;
        }

        protected override void Configure()
        {
            // Queries
            CreateMap<ConfirmationReport, ConfirmationReportViewModel>()
                .ForMember(dest => dest.OwnerCompleteName, opt => opt.ResolveUsing((res, src) => {
                    var user = Task.Run(() => auth.GetUserInfoByUsername(src.OwnerName)).Result;
                    return user.Name + " " + user.Surname;
                }));
            CreateMap<ConfirmationReportDetail, ConfirmationReportDetailViewModel>();

            // Commands
            CreateMap<ConfirmationReport, CR.Domain.Model.ConfirmationReport>();
            CreateMap<ConfirmationReportDetail, CR.Domain.Model.ConfirmationReportDetail>();
            CreateMap<CR.Domain.Model.ConfirmationReport, ConfirmationReport>();
            CreateMap<CR.Domain.Model.ConfirmationReportDetail, ConfirmationReportDetail>();
            CreateMap<CR.Domain.Model.ConfirmationReport, ConfirmationReportViewModel>()
                .ForMember(dest => dest.OwnerCompleteName, opt => opt.ResolveUsing((res, src) => {
                    var user = Task.Run(() => auth.GetUserInfoByUsername(src.OwnerName)).Result;
                    return user.Name + " " + user.Surname;
                }));
            CreateMap<CR.Domain.Model.ConfirmationReportDetail, ConfirmationReportDetailViewModel>();           
        }
    }
}
