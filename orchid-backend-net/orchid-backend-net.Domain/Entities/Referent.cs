using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class Referent : BaseGuidEntity
    {
        public string StageID {  get; set; }
        [ForeignKey(nameof(StageID))]
        public virtual Stage Stage { get; set; }
        //public string StageAttributeID { get; set; }
        //[ForeignKey(nameof(StageAttributeID))]
        //public virtual StageAttribute StageAttribute { get; set; }
        public string Name {  get; set; }
        public double Value_from {  get; set; }
        public double Value_to {  get; set; }
        public int Unit {  get; set; }
        public bool Status {  get; set; }
    }
}
