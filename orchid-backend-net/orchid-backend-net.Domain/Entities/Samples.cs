using orchid_backend_net.Domain.Entities.Base;

namespace orchid_backend_net.Domain.Entities
{
    public class Samples : BaseGuidEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly Dob { get; set; }
        public bool Status {  get; set; }
    }
}
