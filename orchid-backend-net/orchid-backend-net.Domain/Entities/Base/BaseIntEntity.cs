using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace orchid_backend_net.Domain.Entities.Base
{
    public class BaseIntEntity : BaseEntity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override int ID { get; set; }
    }
}
