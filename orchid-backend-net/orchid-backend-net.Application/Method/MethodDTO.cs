using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method
{
    public class MethodDTO : IMapFrom<Domain.Entities.Method>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public MethodDTO Create(string name, string description, string type, bool status)
            => new MethodDTO { 
                Description = description,
                Name = name,
                Type = type,
                Status = status
            };
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Method, MethodDTO>();
        }
    }
}
