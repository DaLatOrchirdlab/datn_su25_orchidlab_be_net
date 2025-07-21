using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Application.Sample;
using orchid_backend_net.Application.Stage;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.Enums;

namespace orchid_backend_net.Application.ExperimentLog
{
    public class ExperimentLogDTO : IMapFrom<ExperimentLogs>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MethodName { get; set; }
        public string Description { get; set; }
        public string TissueCultureBatchName { get; set; }
        public string CurrentStageName { get; set; }
        public List<StageDTO> Stages {  get; set; }
        public List<HybridzationDTO> Hybridizations { get; set; }
        public ExperimentLogStatus Status { get; set; }
        public string? Create_by { get; set; }
        public DateTime? Create_date { get; set; }
        public string? Update_by { get; set; }
        public DateTime? Update_date { get; set; }
        public string? Delete_by { get; set; }
        public DateTime? Delete_date { get; set; }

        public static ExperimentLogDTO Create(string methodName, string tissueCultureBatchName, string description, 
            string Id, List<StageDTO> stageDTOs, List<SampleDTO> sampleDTOs, ExperimentLogStatus status,
            string? createdBy, DateTime? createdDate, string? updatedBy, 
            DateTime? updatedDate, string? deletedBy, DateTime? deletedDate,
            List<HybridzationDTO> hybridzationDTOs, string name)
        {
            return new ExperimentLogDTO
            {
                Id = Id,
                MethodName = methodName,
                Name = name,
                TissueCultureBatchName = tissueCultureBatchName,
                Description = description,
                Stages = stageDTOs,
                Status = status,
                Create_by = createdBy,
                Create_date = createdDate,
                Update_by = updatedBy,
                Update_date = updatedDate,
                Delete_by = deletedBy,
                Delete_date = deletedDate,
                Hybridizations = hybridzationDTOs
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExperimentLogs, ExperimentLogDTO>()
                .ForMember(dest => dest.TissueCultureBatchName, opt => opt.MapFrom(src => src.TissueCultureBatch.Name))
                .ForMember(dest => dest.MethodName, opt => opt.MapFrom(src => src.Method.Name))
                .ForMember(dest => dest.Stages, otp => otp.MapFrom(src => src.Method.Stages))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (ExperimentLogStatus)src.Status))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
                .ForMember(dest => dest.CurrentStageName, opt => opt.MapFrom(src => src.CurrentStageID != null
                    ? src.Method.Stages.Where(s => s.ID == src.CurrentStageID).Select(s => s.Name).FirstOrDefault()
                    : null))
                .ReverseMap();
        }
    }
}
