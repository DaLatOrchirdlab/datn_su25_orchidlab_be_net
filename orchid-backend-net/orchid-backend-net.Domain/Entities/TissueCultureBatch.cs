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
    public class TissueCultureBatch : Entity
    {
        [Key]
        public string ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LabRoom {  get; set; }
        [ForeignKey(nameof(LabRoom))]
        public virtual labRoom LabRoomID { get; set; }
        public bool Status {  get; set; }
    }
}
