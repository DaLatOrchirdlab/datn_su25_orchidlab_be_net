using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class Hybridization : BaseGuidEntity
    {
        public string ParentID {  get; set; }
        [ForeignKey(nameof(ParentID))]
        public virtual Seedling Parent { get; set; }
        public string ExperimentLogID {  get; set; }
        [ForeignKey(nameof(ExperimentLogID))]
        public virtual ExperimentLog ExperimentLog { get; set; }
        public bool Status {  get; set; }
    }
}
