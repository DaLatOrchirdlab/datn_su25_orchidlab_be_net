using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
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
        public ReportDTO Create(string id, string name, string sample, string description, string teachnician, bool status)
            => new ReportDTO
            {
                ID = id,
                Name = name,
                Sample = sample,
                Description = description,
                Technician = teachnician,
                Status = status
            };
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Reports, ReportDTO>()
                .ForMember(dest => dest.Sample, opt => opt.MapFrom(src => src.SampleID));
        }
    }
}
