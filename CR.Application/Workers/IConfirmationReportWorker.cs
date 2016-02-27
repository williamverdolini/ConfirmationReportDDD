using CR.Application.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CR.Application.Workers
{
    public interface IConfirmationReportWorker
    {
        // Commands
        Task<ConfirmationReportViewModel> Save(ConfirmationReportViewModel report);
        Task<ConfirmationReportViewModel> SaveDraft(ConfirmationReportViewModel report);
        // Queries
        Task<ConfirmationReportViewModel> FindByNumber(int reportNumber);
        Task<ConfirmationReportViewModel> FindById(int id);
        Task<List<ConfirmationReportViewModel>> FindAllByOwner(string ownerName, ReportStatus? status);
        Task<int> FindNewReportNumber();
    }
}
