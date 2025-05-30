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
    public class InfectedSample : Entity
    {
        [Key]
        public string ID {  get; set; }
        public string Sample {  get; set; }
        [ForeignKey(nameof(Sample))]
        public virtual Sample SampleID { get; set; }
        public string Disease {  get; set; }
        [ForeignKey(nameof(Disease))]
        public virtual Disease DiseaseID { get; set; }
        public string Status {  get; set; }
    }
}
