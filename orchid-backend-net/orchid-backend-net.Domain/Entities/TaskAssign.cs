using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class TaskAssign : BaseGuidEntity
    {
        public string TechnicianID {  get; set; }
        [ForeignKey(nameof(TechnicianID))]
        public virtual User Technician { get; set; }
        public string TaskID {  get; set; }
        [ForeignKey(nameof(TaskID))]
        public virtual Task Task { get; set; }
        public int Status { get; set; }
    }
}
