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
    public class ExperimentLog : Entity
    {
        [Key]
        public string ID {  get; set; }
        public string Method {  get; set; }
        [ForeignKey("Method")]
        public virtual Method MethodID {  get; set; }
        public string Description { get; set; }
        public string TissueCultureBatch {  get; set; }
        [ForeignKey("TissueCultureBatch")]
        public virtual TissueCultureBatch TissueCultureBatchID {  get; set; }
        //public enum Status
    }
}
