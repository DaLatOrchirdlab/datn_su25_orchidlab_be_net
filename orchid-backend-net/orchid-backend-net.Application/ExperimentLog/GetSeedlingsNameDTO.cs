using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.ExperimentLog
{
    public class GetSeedlingsNameDTO : IMapFrom<Seedlings>
    {
        public string Name { get; set; }
        public static GetSeedlingsNameDTO Create(string name)
        {
            return new GetSeedlingsNameDTO
            {
                Name = name
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Seedlings, GetSeedlingsNameDTO>()
                .ReverseMap();
        }
    }
}
