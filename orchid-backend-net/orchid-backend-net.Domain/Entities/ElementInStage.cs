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
    public class ElementInStage : Entity
    {
        [Key]
        public string ID {  get; set; }
        public string Stage {  get; set; }
        [ForeignKey("Stage")]
        public virtual Stage StageID {  get; set; }
        public string Element { get; set; }
        [ForeignKey("Element")]
        public virtual Element ElementID {  get; set; }
        public bool Status {  get; set; }
    }
}
