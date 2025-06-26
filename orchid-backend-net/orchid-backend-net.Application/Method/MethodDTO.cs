using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.Method
{
    public class MethodDTO : IMapFrom<Methods>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public MethodDTO Create(string name, string description, string type, bool status)
            => new MethodDTO
            {
                Description = description,
                Name = name,
                Type = type,
                Status = status
            };
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Methods, MethodDTO>();
        }
    }
}
