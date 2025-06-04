using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class InfectedSample : BaseGuidEntity
    {
        public string SampleID {  get; set; }
        [ForeignKey(nameof(SampleID))]
        public virtual Sample Sample { get; set; }
        public string DiseaseID {  get; set; }
        [ForeignKey(nameof(DiseaseID))]
        public virtual Disease Disease { get; set; }
        public string Status {  get; set; }
    }
}
