using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class ElementInStage : BaseGuidEntity
    {
        public string StageID {  get; set; }
        [ForeignKey(nameof(StageID))]
        public virtual Stage Stage {  get; set; }
        public string ElementID { get; set; }
        [ForeignKey(nameof(ElementID))]
        public virtual Element Element {  get; set; }
        public bool Status {  get; set; }
    }
}
