using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class ReportAttributes : BaseGuidEntity
    {
        public virtual ICollection<Referents> Referents { get; set; } = [];
        public string ReportID { get; set; }
        [ForeignKey(nameof(ReportID))]
        public virtual Reports Report { get; set; }
        //public enum Status
        public int Status { get; set; } // 0: Pending, 1: Approved, 2: Rejected, 3: In Progress, 4: Completed

    }
}
