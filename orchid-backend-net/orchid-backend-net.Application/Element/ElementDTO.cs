using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;

namespace orchid_backend_net.Application.Element
{
    public class ElementDTO : IMapFrom<orchid_backend_net.Domain.Entities.Element>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public static ElementDTO Create(string id, string name, string description)
        {
            return new ElementDTO
            {
                ID = id,
                Name = name,
                Description = description
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<orchid_backend_net.Domain.Entities.Element, ElementDTO>();
        }
    }
}
