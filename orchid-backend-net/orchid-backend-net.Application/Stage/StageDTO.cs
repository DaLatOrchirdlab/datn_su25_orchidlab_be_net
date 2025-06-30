using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Stage
{
    public class StageDTO : IMapFrom<Stages>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DateOfProcessing { get; set; }
        public StageDTO() { }   
        public StageDTO(string name, string description, int dateOfProcessing)
        {
            Name = name;
            Description = description;
            DateOfProcessing = dateOfProcessing;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Stages, StageDTO>()
                .ReverseMap();
        }
    }
}
