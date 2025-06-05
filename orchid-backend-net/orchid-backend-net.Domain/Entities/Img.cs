using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class Img : BaseGuidEntity
    {
        public string Url {  get; set; }
        public string ReportID {  get; set; }
        [ForeignKey(nameof(ReportID))]
        public virtual Report Report { get; set; }
        public bool Status {  get; set; }
    }
}
