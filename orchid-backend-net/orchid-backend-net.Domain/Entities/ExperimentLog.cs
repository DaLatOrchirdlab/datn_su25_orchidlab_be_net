using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class ExperimentLog : BaseGuidEntity
    {
        public int MethodID {  get; set; }
        [ForeignKey(nameof(MethodID))]
        public virtual Method Method {  get; set; }
        public string Description { get; set; }
        public string TissueCultureBatchID {  get; set; }
        [ForeignKey(nameof(TissueCultureBatchID))]
        public virtual TissueCultureBatch TissueCultureBatch {  get; set; }
        //public enum Status
    }
}
