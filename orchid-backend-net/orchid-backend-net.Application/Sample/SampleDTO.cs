using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Application.ReportAttribute;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.Sample
{
    public class SampleDTO : IMapFrom<Samples>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly Dob { get; set; }
        public Domain.Enums.SamplesStatus StatusEnum { get; set; }
        public List<ReportAttributesDTO> ReportAttributes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Samples, SampleDTO>()
                .ForMember(dest => dest.ReportAttributes, opt => opt.MapFrom(src => 
                    src.Reports
                        .Where(x => x.IsLatest)
                        .SelectMany(report => report.ReportAttributes)))
                .ReverseMap();
        }
    }
}
