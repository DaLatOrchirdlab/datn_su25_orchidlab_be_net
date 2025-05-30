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
        public int ID { get; set; }
        public string Technician {  get; set; }
        [ForeignKey(nameof(Technician))]
        public virtual User TechnicianID { get; set; }
        public string Task {  get; set; }
        [ForeignKey(nameof(Task))]
        public virtual Task TaskID { get; set; }
        public bool Status { get; set; }
    }
}
