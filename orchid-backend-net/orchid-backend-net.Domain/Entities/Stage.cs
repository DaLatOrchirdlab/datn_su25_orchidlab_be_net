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
    public class Stage : Entity
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Method {  get; set; }
        [ForeignKey(nameof(Method))]
        public virtual Method MethodID { get; set; }
        public bool Status {  get; set; }
    }
}
