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
        public string StageID {  get; set; }
        [ForeignKey("StageID")]
        public virtual Stage Stage {  get; set; }
        public string ElementID { get; set; }
        [ForeignKey("ElementID")]
        public virtual Element Element {  get; set; }
        public bool Status {  get; set; }
    }
}
