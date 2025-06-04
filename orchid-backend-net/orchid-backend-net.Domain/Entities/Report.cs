using orchid_backend_net.Domain.Entities.Base;

namespace orchid_backend_net.Domain.Entities
{
    public class Report : BaseGuidEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Technician {  get; set; }
        public bool Status {  get; set; }
    }
}
