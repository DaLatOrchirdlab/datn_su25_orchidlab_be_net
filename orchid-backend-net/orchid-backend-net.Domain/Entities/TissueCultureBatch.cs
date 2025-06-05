using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class TissueCultureBatch : BaseGuidEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LabRoomID {  get; set; }
        [ForeignKey(nameof(LabRoomID))]
        public virtual LabRoom LabRoom { get; set; }
        public bool Status {  get; set; }
    }
}
