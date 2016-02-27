using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CR.Domain.Model
{
    public enum ReportStatus
    {
        Draft = 0,
        Completed = 1
    }

    public enum InterventionMode
    {
        OnSite = 0,
        FromOffice = 1,
        ByPhone = 2,
        Other = 3
    }
}
