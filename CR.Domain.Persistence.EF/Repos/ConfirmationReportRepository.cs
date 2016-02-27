using CR.Domain.Model;
using CR.Infrastructure.Mappings;
using CR.Infrastructure.Repo;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CR.Domain.Persistence.EF.Repos
{
    public class ConfirmationReportRepository : IRepository<ConfirmationReport>
    {
        private readonly ConfirmReportContext db;
        private readonly IMapper mapper;

        public ConfirmationReportRepository(ConfirmReportContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<ConfirmationReport> GetById(int id)
        {
            //var efEntity = await db.Reports.FindAsync(id);
            var efEntity = await db.Reports.AsNoTracking().FirstOrDefaultAsync(r => r.Id.Equals(id));
            return mapper.Map<ConfirmationReport>(efEntity);
        }

        public async Task<ConfirmationReport> Save(ConfirmationReport aggregateRoot)
        {
            var report = mapper.Map<Models.ConfirmationReport>(aggregateRoot);
            if (aggregateRoot.Id > 0)
                Update(db, report);
            else
                db.Reports.Add(report);
            await db.SaveChangesAsync();
            return mapper.Map<ConfirmationReport>(report);
        }

        private static void Update(ConfirmReportContext db, Models.ConfirmationReport report)
        {
            //see: http://www.entityframeworktutorial.net/EntityFramework5/update-entity-graph-using-dbcontext.aspx
            var currentReport = db.Reports.AsNoTracking().FirstOrDefault(r => r.Id.Equals(report.Id));
            db.Reports.Attach(report);
            db.Entry(report).State = EntityState.Modified;
            report.Details.Where(d => d.Id > 0).ToList().ForEach(d => { db.Entry(d).State = EntityState.Modified; });
            report.Details.Where(d => d.Id.Equals(0)).ToList().ForEach(d => { db.Entry(d).State = EntityState.Added; });
            currentReport.Details.Where(d => !report.Details.Any(nr => nr.Id.Equals(d.Id))).ToList().ForEach(d => {
                var newD = new Models.ConfirmationReportDetail { Id = d.Id };
                db.ReportDetails.Attach(newD);
                db.ReportDetails.Remove(newD);
            });
        }
    }

}
