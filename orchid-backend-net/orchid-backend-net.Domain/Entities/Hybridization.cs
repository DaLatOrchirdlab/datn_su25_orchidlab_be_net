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
    public class Hybridization : Entity
    {
        [Key]
        public string ID {  get; set; }
        public string ParentID {  get; set; }
        [ForeignKey("ParentID")]
        public virtual Seedling Parent { get; set; }
        public string ExperimentLogID {  get; set; }
        [ForeignKey("ExperimentLogID")]
        public virtual ExperimentLog ExperimentLog { get; set; }
        public bool Status {  get; set; }
    }
}
