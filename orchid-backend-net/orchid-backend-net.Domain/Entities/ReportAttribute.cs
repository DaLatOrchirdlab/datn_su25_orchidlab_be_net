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
    public class ReportAttribute : Entity
    {
        [Key]
        public string ID {  get; set; }
        public string ReferentDataID {  get; set; }
        [ForeignKey(nameof(ReferentDataID))]
        public virtual SeedlingAttribute ReferentData {  get; set; }
        public string Report {  get; set; }
        [ForeignKey(nameof(Report))]
        public virtual Report ReportID { get; set; }
        //public enum Status
    }
}
