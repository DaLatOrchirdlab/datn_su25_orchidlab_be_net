using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.ExperimentLog
{
    public class ExperimentLogDTO : IMapFrom<ExperimentLogs>
    {
        public string Id { get; set; }
        public string MethodName { get; set; }
        public string Description { get; set; }
        public string TissueCultureBatchName { get; set; }

        public static ExperimentLogDTO Create(string methodName, string tissueCultureBatchName, string description, string Id)
        {
            return new ExperimentLogDTO
            {
                Id = Id,
                MethodName = methodName,
                TissueCultureBatchName = tissueCultureBatchName,
                Description = description
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExperimentLogs, ExperimentLogDTO>()
                .ForMember(dest => dest.TissueCultureBatchName, opt => opt.MapFrom(src => src.TissueCultureBatch.Name))
                .ForMember(dest => dest.MethodName, opt => opt.MapFrom(src => src.Method.Name));
        }
    }
}
