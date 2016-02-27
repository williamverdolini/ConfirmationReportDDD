using CR.Application.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CR.Application.Abstractions.Services
{
    public interface IConfirmationReportQueryService
    {
        Task<ConfirmationReportViewModel> FindByNumber(int reportNumber);
        Task<ConfirmationReportViewModel> FindById(int id);
        Task<List<ConfirmationReportViewModel>> FindAllByOwner(string ownerName, ReportStatus? status);
        Task<int> FindNewReportNumber();
    }
}
