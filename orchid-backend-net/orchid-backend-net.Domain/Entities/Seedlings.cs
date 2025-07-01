using orchid_backend_net.Domain.Entities.Base;

namespace orchid_backend_net.Domain.Entities
{
    public class Seedlings : BaseSoftDelete
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Mother {  get; set; }
        public string? Father { get; set; }
        public DateOnly Dob {  get; set; }
        public virtual ICollection<Characteristics> Characteristics { get; set; } = [];
        public virtual ICollection<Hybridizations> Hybridizations { get; set; } = [];
    }
}
