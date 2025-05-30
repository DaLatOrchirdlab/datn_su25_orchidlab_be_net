using orchid_backend_net.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Domain.Entities
{
    public class Disease : Entity
    {
        [Key]
        public string ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string solution {  get; set; }
        public int InfectedRate {  get; set; }
        public bool Status {  get; set; }
    }
}
