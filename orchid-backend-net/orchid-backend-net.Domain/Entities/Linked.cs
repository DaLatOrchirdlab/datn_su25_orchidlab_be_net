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
    public class Linked : Entity
    {
        [Key]
        public int ID { get; set; }
        public string Task {  get; set; }
        [ForeignKey(nameof(Task))]
        public virtual Task TaskID { get; set; }
        public string Sample {  get; set; }
        [ForeignKey(nameof(Sample))]
        public virtual Sample SampleID { get; set; }
        public string ExperimentLog {  get; set; }
        [ForeignKey(nameof(ExperimentLog))]
        public virtual ExperimentLog ExperimentLogID { get; set; }
        public bool Status {  get; set; }
    }
}
