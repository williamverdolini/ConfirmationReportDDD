using CR.Application.Abstractions.Models;
using CR.Application.Abstractions.Services;
using CR.Domain.Model;
using CR.Infrastructure;
using CR.Infrastructure.Mappings;
using CR.Infrastructure.Repo;
using System;
using System.Threading.Tasks;

namespace CR.Domain.Persistence.EF.Services
{
    public class ConfirmationReportCommandService : IConfirmationReportCommandService
    {
        private readonly IRepository<ConfirmationReport> repo;
        private readonly IMapper mapper;

        public ConfirmationReportCommandService(IRepository<ConfirmationReport> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<ConfirmationReportViewModel> Save(ConfirmationReportViewModel report)
        {
            Contract.Requires<ArgumentNullException>(report != null, "report");

            var domainReport = report.Id.Equals(0) ? new ConfirmationReport() : await repo.GetById(report.Id);
            domainReport.Save(report);
            await repo.Save(domainReport);

            return mapper.Map<ConfirmationReportViewModel>(domainReport);
        }

        public async Task<ConfirmationReportViewModel> SaveDraft(ConfirmationReportViewModel report)
        {
            Contract.Requires<ArgumentNullException>(report != null, "report");

            var domainReport = report.Id.Equals(0) ? new ConfirmationReport() : await repo.GetById(report.Id);
            domainReport.SaveDraft(report);
            await repo.Save(domainReport);

            return mapper.Map<ConfirmationReportViewModel>(domainReport);
        }
    }
}
