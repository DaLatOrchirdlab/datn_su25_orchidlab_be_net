using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Application.Images;
using orchid_backend_net.Application.ReportAttribute;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.Report
{
    public class ReportDTO : IMapFrom<Reports>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sample { get; set; }
        public string Technician { get; set; }
        public bool Status { get; set; }
        public List<ReportAttributesDTO> ReportAttributes { get; set; }
        public List<ImagesDTO> Imgs { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Reports, ReportDTO>()
                .ForMember(dest => dest.Sample, opt => opt.MapFrom(src => src.SampleID))
                .ForMember(dest => dest.Technician, opt => opt.MapFrom(src => src.Technician.Name))
                .ForMember(dest => dest.ReportAttributes, opt => opt.MapFrom(src => src.ReportAttributes))
                .ForMember(dest => dest.Imgs, otp => otp.MapFrom(src => src.Imgs))
                .ReverseMap();
        }
    }
}
