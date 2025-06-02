using orchid_backend_net.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Domain.Entities
{
    public class Report : Entity
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Technician {  get; set; }
        public bool Status {  get; set; }
    }
}
