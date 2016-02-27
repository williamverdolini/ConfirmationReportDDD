using CR.Application.Abstractions.Models;
using CR.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Domain.Model
{
    public class ConfirmationReport : IDomainEntity
    {
        //private readonly IList<ConfirmationReportDetail> Details;
        // per AutoMapper
        protected IList<ConfirmationReportDetail> _Details { get; private set; }
        public IEnumerable<ConfirmationReportDetail> Details { get { return _Details.AsEnumerable(); } }

        public ConfirmationReport()
        {
            this._Details = new List<ConfirmationReportDetail>();
            Status = ReportStatus.Draft;
        }

        public int Id { get; private set; }
        public int ReportNumber { get; private set; }
        public DateTime ReportDate { get; private set; }
        public string OwnerName { get; private set; }
        public string OtherInterventionOps { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerRepresentative { get; private set; }
        public InterventionMode InterventionMode { get; private set; }
        public string OtherInterventionMode { get; private set; }
        public string Notes { get; private set; }
        public ReportStatus Status { get; private set; }

        public void SaveDraft(ConfirmationReportViewModel report)
        {
            Status = ReportStatus.Draft;
            PopulateReport(report);
            CheckInvariants();
        }

        public void Save(ConfirmationReportViewModel report)
        {
            this.Status = ReportStatus.Completed;
            PopulateReport(report);
            CheckInvariants();
        }

        private void PopulateReport(ConfirmationReportViewModel report)
        {
            ReportNumber = report.ReportNumber;
            ReportDate = report.ReportDate;
            OwnerName = report.OwnerName;
            OtherInterventionOps = report.OtherInterventionOps;
            CustomerName = report.CustomerName;
            CustomerRepresentative = report.CustomerRepresentative;
            InterventionMode = (InterventionMode)Enum.Parse(typeof(InterventionMode), report.InterventionMode.ToString());
            OtherInterventionMode = report.OtherInterventionMode;
            Notes = report.Notes;
            _Details.Clear();
            if(report.Details != null)
                report.Details.ToList().ForEach(x =>
                {
                    _Details.Add(new ConfirmationReportDetail
                    {
                        Id = x.Id,
                        ReportId = x.ReportId,
                        Date = x.Date,
                        FromTime = x.FromTime,
                        ToTime = x.ToTime,
                        Description = x.Description
                    });
                });
        }

        private void CheckInvariants()
        {
            if (CheckOverlappedDetails())
                throw new OverlappedDetailException();
        }

        public void AddDetail(ConfirmationReportDetail detail)
        {
            _Details.Add(detail);
        }

        internal bool CheckOverlappedDetails()
        {
            return _Details.GroupBy(d => d, new OverlappingCompare()).Where(grp => grp.Count() > 1).Any();
        }

        // due dettagli A e B si sovrappongo se:
        // ilgiorno è lo stesso
        // A.From <= B.From && A.To >= B.From
        // ||
        // A.From >= B.From && B.To >= A.From
        private class OverlappingCompare : IEqualityComparer<ConfirmationReportDetail>
        {
            public bool Equals(ConfirmationReportDetail x, ConfirmationReportDetail y)
            {
                return  (x.FromTime <= y.FromTime && x.ToTime > y.FromTime) || 
                        (x.FromTime >= y.FromTime && y.ToTime > x.FromTime) ;
            }
            public int GetHashCode(ConfirmationReportDetail d)
            {
                return (d.Date.Year + d.Date.DayOfYear).GetHashCode();
            }
        }


    }
}
