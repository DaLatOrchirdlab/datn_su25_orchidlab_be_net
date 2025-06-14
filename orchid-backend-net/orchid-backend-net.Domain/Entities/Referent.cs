﻿using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class Referent : BaseIntEntity
    {
        public string StageID {  get; set; }
        [ForeignKey(nameof(StageID))]
        public virtual Stage Stage { get; set; }
        public string StageAttributeID { get; set; }
        [ForeignKey(nameof(StageAttributeID))]
        public virtual StageAttribute StageAttribute { get; set; }
        public bool Status {  get; set; }
    }
}
