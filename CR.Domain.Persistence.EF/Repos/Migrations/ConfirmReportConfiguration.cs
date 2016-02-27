using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CR.Domain.Persistence.EF.Repos.Migrations
{
    internal sealed class ConfirmReportConfiguration : DbMigrationsConfiguration<ConfirmReportContext>
    {
        public ConfirmReportConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ConfirmRep.Repositories.ConfirmReportContext";
        }
    }
}
