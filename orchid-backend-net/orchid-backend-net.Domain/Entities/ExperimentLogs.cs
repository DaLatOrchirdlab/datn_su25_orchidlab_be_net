﻿using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class ExperimentLogs : BaseGuidEntity
    {
        public string MethodID {  get; set; }
        [ForeignKey(nameof(MethodID))]
        public virtual Methods Method {  get; set; }
        public string Description { get; set; }
        public string TissueCultureBatchID {  get; set; }
        [ForeignKey(nameof(TissueCultureBatchID))]
        public virtual TissueCultureBatches TissueCultureBatch {  get; set; }
        //public enum Status
        public int Status {  get; set; }
    }
}
