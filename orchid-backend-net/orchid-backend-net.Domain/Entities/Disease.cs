using orchid_backend_net.Domain.Entities.Base;

namespace orchid_backend_net.Domain.Entities
{
    public class Disease : BaseGuidEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string solution {  get; set; }
        public int InfectedRate {  get; set; }
        public bool Status {  get; set; }
    }
}
