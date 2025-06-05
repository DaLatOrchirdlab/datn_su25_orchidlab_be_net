using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class Characteristic : BaseGuidEntity
    {
        public string SeedlingAttributeID { get; set; }
        [ForeignKey(nameof(SeedlingAttributeID))]
        public virtual SeedlingAttribute SeedlingAttribute {  set; get; }
        public string SeedlingID { get; set; }
        [ForeignKey(nameof(SeedlingID))]
        public virtual Seedling Seedling {  set; get; }
        public double Value {  get; set; }
        //public enum Unit
        public bool Status {  get; set; }
    }
}
