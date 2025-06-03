using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class TaskAttribute : BaseGuidEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string TaskID {  get; set; }
        [ForeignKey(nameof(TaskID))]
        public virtual Task Task {  get; set; }
        public double Value {  get; set; }
        //public enum Unit
        public bool Status {  get; set; }
    }
}
