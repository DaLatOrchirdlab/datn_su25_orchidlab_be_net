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
        public string Parent {  get; set; }
        [ForeignKey("Parent")]
        public virtual Seedling ParentID { get; set; }
        public string ExperimentLog {  get; set; }
        [ForeignKey("ExperimentLog")]
        public virtual ExperimentLog ExperimentLogID { get; set; }
        public bool Status {  get; set; }
    }
}
