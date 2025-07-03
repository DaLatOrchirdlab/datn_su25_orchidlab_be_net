using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.ExperimentLog
{
    public class HybridzationDTO : IMapFrom<Hybridizations>
    {
        public bool IsMother { get; set; }
        public GetSeedlingsNameDTO Seedling { get; set; }

        public static HybridzationDTO Create(bool isMother, GetSeedlingsNameDTO getSeedlingsNameDTO)
        {
            return new HybridzationDTO
            {
                IsMother = isMother,
                Seedling = getSeedlingsNameDTO
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Hybridizations, HybridzationDTO>()
                //.ForMember(dest => dest.IsMother, opt => opt.MapFrom(src => src.IsMother))
                .ForMember(dest => dest.Seedling, opt => opt.MapFrom(src => src.Parent))
                .ReverseMap();
        }
    }
}
