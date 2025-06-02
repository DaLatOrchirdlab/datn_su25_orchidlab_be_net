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
    public class Img : Entity
    {
        [Key]
        public string ID {  get; set; }
        public string Url {  get; set; }
        public string ReportID {  get; set; }
        [ForeignKey("ReportID")]
        public virtual Report Report { get; set; }
        public bool Status {  get; set; }
    }
}
