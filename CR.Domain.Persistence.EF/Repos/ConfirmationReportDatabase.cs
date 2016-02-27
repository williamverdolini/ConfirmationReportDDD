using CR.Domain.Persistence.EF.Models;
using CR.Infrastructure.Db;
using System;
using System.Linq;

namespace CR.Domain.Persistence.EF.Repos
{
    public class ConfirmationReportDatabase : IDatabase<ConfirmationReport>, IDisposable
    {
        private readonly ConfirmReportContext context;

        public ConfirmationReportDatabase()
        {
            context = new ConfirmReportContext();
            //context.Configuration.LazyLoadingEnabled = false;
            //context.Configuration.ProxyCreationEnabled = false;
        }

        public IQueryable<ConfirmationReport> DbSet
        {
            get {  return context.Reports; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();
        }
    }
}
