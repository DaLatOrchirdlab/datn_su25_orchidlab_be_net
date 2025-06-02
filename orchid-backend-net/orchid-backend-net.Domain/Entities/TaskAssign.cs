using orchid_backend_net.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Domain.Entities
{
    public class TaskAssign : Entity
    {
        [Key]
        public string ID { get; set; }
        public string TechnicianID {  get; set; }
        [ForeignKey(nameof(TechnicianID))]
        public virtual User Technician { get; set; }
        public string TaskID {  get; set; }
        [ForeignKey(nameof(TaskID))]
        public virtual Task Task { get; set; }
        public bool Status { get; set; }
    }
}
