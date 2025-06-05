using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class Linked : BaseIntEntity
    {
        public string TaskID {  get; set; }
        [ForeignKey(nameof(TaskID))]
        public virtual Task Task { get; set; }
        public string SampleID {  get; set; }
        [ForeignKey(nameof(SampleID))]
        public virtual Sample Sample { get; set; }
        public string ExperimentLogID {  get; set; }
        [ForeignKey(nameof(ExperimentLogID))]
        public virtual ExperimentLog ExperimentLog { get; set; }
        public bool Status {  get; set; }
    }
}
