using CR.Infrastructure.Model;
using System;

namespace CR.Domain.Model
{
    public class ConfirmationReportDetail : IDomainEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public string Description { get; set; }
        public int ReportId { get; set; }
    }
}
