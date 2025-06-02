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
    public class Characteristic : Entity
    {
        [Key]
        public string ID {  get; set; }
        public string SeedlingAttributeID { get; set; }
        [ForeignKey("SeedlingAttributeID")]
        public virtual SeedlingAttribute SeedlingAttribute {  set; get; }
        public string SeedlingID { get; set; }
        [ForeignKey("SeedlingID")]
        public virtual Seedling Seedling {  set; get; }
        public double Value {  get; set; }
        //public enum Unit
        public bool Status {  get; set; }
    }
}
