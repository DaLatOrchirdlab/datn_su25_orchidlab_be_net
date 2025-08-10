using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class Reports : BaseSoftDelete
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string TechnicianID { get; set; }
        [ForeignKey(nameof(TechnicianID))]
        public virtual Users Technician { get; set; }
        public string SampleID { get; set; }
        [ForeignKey(nameof(SampleID))]
        public virtual Samples Sample { get; set; }
        public bool IsLatest { get; set; } // Indicates if this report is the latest for the sample
        public bool Status { get; set; }
        public virtual ICollection<ReportAttributes> ReportAttributes { get; set; } = [];
        public virtual ICollection<Imgs> Imgs { get; set; } = [];
    }
}
