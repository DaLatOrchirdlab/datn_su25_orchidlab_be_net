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
        public List<ElementInStageDTO> Elements { get; set; }
        public StageDTO() { }   
        public StageDTO(string name, string description, int dateOfProcessing , List<ElementInStageDTO> elements)
        {
            Name = name;
            Description = description;
            DateOfProcessing = dateOfProcessing;
            Elements = elements;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Stages, StageDTO>()
                .ForMember(dest => dest.Elements, otp => otp.MapFrom(src => src.ElementInStage))
                .ReverseMap();
        }
    }
}
