using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class Stage : BaseGuidEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MethodID {  get; set; }
        [ForeignKey(nameof(MethodID))]
        public virtual Method Method { get; set; }
        public bool Status {  get; set; }
    }
}
