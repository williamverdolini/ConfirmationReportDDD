using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CR.Domain.Persistence.EF.Models
{
    public class ConfirmationReport : IAuditable
    {
        public ConfirmationReport()
        {
            this.Details = new List<ConfirmationReportDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        [Required]
        public Int32 ReportNumber { get; set; }
        [Required]
        public DateTime ReportDate { get; set; }
        [Required]
        public string OwnerName { get; set; }
        public string OtherInterventionOps { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public string CustomerRepresentative { get; set; }
        [Required]
        public Model.InterventionMode InterventionMode { get; set; }
        public string OtherInterventionMode { get; set; }
        public string Notes { get; set; }
        public Model.ReportStatus Status { get; set; }
        public virtual ICollection<ConfirmationReportDetail> Details { get; protected set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
