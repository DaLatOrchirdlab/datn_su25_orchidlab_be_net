using orchid_backend_net.Domain.Entities.Base;

namespace orchid_backend_net.Domain.Entities
{
    //cân nhắc để bỏ parent và parent 1 trong seedlings
    public class SeedlingParents 
        //: BaseGuidEntity
    {
        public string ChildSeedlingID { get; set; }
        public string ParentSeedlingID { get; set; }
        public string RoleOfParent { get; set; } // e.g., "Mother", "Father", "Guardian"
        public string Method { get; set; } // e.g., "Natural", "Artificial"
    }
}
