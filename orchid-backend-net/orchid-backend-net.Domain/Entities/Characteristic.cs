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
        public string SeedlingAttribute { get; set; }
        [ForeignKey("SeedlingAttribute")]
        public virtual SeedlingAttribute SeedlingAttributeID {  set; get; }
        public string Seedling { get; set; }
        [ForeignKey("Seedling")]
        public virtual Seedling SeedlingID {  set; get; }
        public double Value {  get; set; }
        //public enum Unit
        public bool Status {  get; set; }
    }
}
