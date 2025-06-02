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
        public string SampleID {  get; set; }
        [ForeignKey(nameof(SampleID))]
        public virtual Sample Sample { get; set; }
        public string DiseaseID {  get; set; }
        [ForeignKey(nameof(DiseaseID))]
        public virtual Disease Disease { get; set; }
        public string Status {  get; set; }
    }
}
