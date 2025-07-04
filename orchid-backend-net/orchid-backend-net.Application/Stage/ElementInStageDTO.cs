
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
    public class ElementInStageDTO : IMapFrom<ElementInStage>
    {

        public Elements Elements { get; set; }
        public ElementInStageDTO() { }
        public ElementInStageDTO(Elements elements)
        {
            Elements = elements;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ElementInStageDTO, ElementInStage>()
                .ForMember(dest => dest.Element, otp => otp.MapFrom(src => src.Elements))
                .ReverseMap();
        }
    }
}
