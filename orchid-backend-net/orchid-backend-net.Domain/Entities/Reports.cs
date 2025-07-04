﻿using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class Reports : BaseGuidEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string TechnicianID {  get; set; }
        public string SampleID { get; set; }
        [ForeignKey(nameof(SampleID))]
        public virtual Samples Sample { get; set; }
        public bool Status {  get; set; }
    }
}
