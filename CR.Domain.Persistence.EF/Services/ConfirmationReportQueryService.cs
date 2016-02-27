using CR.Application.Abstractions.Models;
using CR.Application.Abstractions.Services;
using CR.Domain.Persistence.EF.Models;
using CR.Infrastructure;
using CR.Infrastructure.Db;
using CR.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CR.Domain.Persistence.EF.Services
{
    //public class A : ConfirmationReportQueryService
    //{
    //    public void metodo()
    //    {
    //        this.Fi
    //    }
    //}

    public class ConfirmationReportQueryService : IConfirmationReportQueryService
    {
        private readonly IDatabase<ConfirmationReport> db;
        private readonly IMapper mapper;

        public ConfirmationReportQueryService(IDatabase<ConfirmationReport> db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<ConfirmationReportViewModel>> FindAllByOwner(string ownerName, ReportStatus? status)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(ownerName), "ownerName");

            //List<ConfirmationReport> reportsList = await FindAllByOwnerDomain(ownerName, status);
            var reports = db.DbSet.AsNoTracking().Where(r => r.OwnerName.Equals(ownerName));
            if (status != null)
                reports = reports.Where(r => r.Status.ToString() == status.Value.ToString());
            List<ConfirmationReport> reportsList = await reports.ToListAsync();

            return mapper.Map<List<ConfirmationReportViewModel>>(reportsList);
        }

        internal async Task<List<ConfirmationReport>> FindAllByOwnerDomain(string ownerName, ReportStatus? status)
        {
            var reports = db.DbSet.AsNoTracking().Where(r => r.OwnerName.Equals(ownerName));
            if (status != null)
                reports = reports.Where(r => r.Status.ToString() == status.Value.ToString());
            List<ConfirmationReport> reportsList = await reports.ToListAsync();
            return reportsList;
        }

        public async Task<ConfirmationReportViewModel> FindById(int id)
        {
            Contract.Requires<ArgumentException>(id > 0, "reportId");

            var result = await db.DbSet.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<ConfirmationReportViewModel>(result);
        }

        public async Task<ConfirmationReportViewModel> FindByNumber(int reportNumber)
        {
            Contract.Requires<ArgumentException>(reportNumber > 0, "reportNumber");

            var result = await db.DbSet.AsNoTracking().FirstOrDefaultAsync(r => r.ReportNumber.Equals(reportNumber));
            return mapper.Map<ConfirmationReportViewModel>(result);
        }

        public async Task<int> FindNewReportNumber()
        {
            int reportNumber = 0;
            if (await db.DbSet.AnyAsync())
                reportNumber = await db.DbSet.AsNoTracking().MaxAsync(r => r.ReportNumber);
            return ++reportNumber;
        }
    }
}
