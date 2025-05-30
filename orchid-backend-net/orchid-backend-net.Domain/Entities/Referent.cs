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
    public class Referent : Entity
    {
        [Key]
        public int ID { get; set; }
        public string Stage {  get; set; }
        [ForeignKey(nameof(Stage))]
        public virtual Stage StageID { get; set; }
        public string StageAttribute { get; set; }
        [ForeignKey(nameof(StageAttribute))]
        public virtual StageAttribute StageAttributeID { get; set; }
        public bool Status {  get; set; }
    }
}
