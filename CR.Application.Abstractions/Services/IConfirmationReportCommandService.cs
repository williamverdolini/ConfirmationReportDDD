using CR.Application.Abstractions.Models;
using System.Threading.Tasks;

namespace CR.Application.Abstractions.Services
{
    public interface IConfirmationReportCommandService
    {
        Task<ConfirmationReportViewModel> Save(ConfirmationReportViewModel report);
        Task<ConfirmationReportViewModel> SaveDraft(ConfirmationReportViewModel report);
    }
}
