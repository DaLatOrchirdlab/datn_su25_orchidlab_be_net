using orchid_backend_net.Domain.Entities.Base;

namespace orchid_backend_net.Domain.Entities
{
    public class SeedlingAttribute : BaseGuidEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status {  get; set; }
    }
}
