using CR.Application.Abstractions.Models;
using CR.Application.Abstractions.Services;
using CR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CR.Application.Workers
{
    public class ConfirmationReportWorker : IConfirmationReportWorker
    {
        private readonly IConfirmationReportCommandService command;
        private readonly IConfirmationReportQueryService query;

        public ConfirmationReportWorker(IConfirmationReportCommandService command, IConfirmationReportQueryService query )
        {
            Contract.Requires<ArgumentNullException>(command != null, "IConfirmationReportCommandService command");
            Contract.Requires<ArgumentNullException>(query != null, "IConfirmationReportQueryService query");
            this.command = command;
            this.query = query;
        }

        public async Task<ConfirmationReportViewModel> SaveDraft(ConfirmationReportViewModel report)
        {
            Contract.Requires<ArgumentNullException>(report != null, "report");
            return await command.SaveDraft(report);
        }

        public async Task<ConfirmationReportViewModel> Save(ConfirmationReportViewModel report)
        {
            Contract.Requires<ArgumentNullException>(report != null, "report");
            return await command.Save(report);
        }

        public async Task<ConfirmationReportViewModel> FindByNumber(int reportNumber)
        {
            Contract.Requires<ArgumentException>(reportNumber > 0, "reportNumber");
            return await query.FindByNumber(reportNumber);
        }

        public async Task<ConfirmationReportViewModel> FindById(int reportId)
        {
            Contract.Requires<ArgumentException>(reportId > 0, "reportId");
            return await query.FindById(reportId);
        }

        public async Task<int> FindNewReportNumber()
        {
            return await query.FindNewReportNumber();
        }

        public async Task<List<ConfirmationReportViewModel>> FindAllByOwner(string ownerName, ReportStatus? status)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(ownerName), "ownerName");
            return await query.FindAllByOwner(ownerName, status);
        }

    }

}