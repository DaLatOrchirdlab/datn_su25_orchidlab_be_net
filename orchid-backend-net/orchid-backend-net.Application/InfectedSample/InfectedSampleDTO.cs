using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Application.Disease;
using orchid_backend_net.Application.Sample;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.Enums;

namespace orchid_backend_net.Application.InfectedSample
{
    public class InfectedSampleDTO : IMapFrom<InfectedSamples>
    {
        public string ID { get; set; }
        public SampleDTO Sample { get; set; }
        public DiseaseDTO Disease { get; set; }
        public string InfectedLevel { get; set; }
        public string TreatmentStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InfectedSamples, InfectedSampleDTO>()
                .ForMember(dest => dest.Sample, opt => opt.MapFrom(src => src.Sample))
                .ForMember(dest => dest.Disease, opt => opt.MapFrom(src => src.Disease))
                .ForMember(dest => dest.InfectedLevel,
                    opt => opt.MapFrom(src => ((InfectedSampleInfectedLevel)src.InfectedLevel).ToString()))
                .ForMember(dest => dest.TreatmentStatus,
                    opt => opt.MapFrom(src => ((InfectedSampleTreatmentStatus)src.TreatmentStatus).ToString()));

            profile.CreateMap<InfectedSampleDTO, InfectedSamples>()
                .ForMember(dest => dest.InfectedLevel, opt => opt.MapFrom(src => (int)Enum.Parse<InfectedSampleInfectedLevel>(src.InfectedLevel)))
                .ForMember(dest => dest.TreatmentStatus, opt => opt.MapFrom(src => (int)Enum.Parse<InfectedSampleTreatmentStatus>(src.TreatmentStatus)));
        }
    }
}
