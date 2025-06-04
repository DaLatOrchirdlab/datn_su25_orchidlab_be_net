using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class ReportAttribute : BaseGuidEntity
    {
        public string ReferentDataID {  get; set; }
        [ForeignKey(nameof(ReferentDataID))]
        public virtual SeedlingAttribute ReferentData {  get; set; }
        public string ReportID { get; set; }
        [ForeignKey(nameof(ReportID))]
        public virtual Report Report { get; set; }
        //public enum Status
    }
}
