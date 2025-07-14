using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.TaskAssign
{
    public class TaskAssignDTO : IMapFrom<TasksAssign>
    {
        public string TechnicianName { get; set; }
        public string TaskID { get; set; }
        public int Status { get; set; }

        public TaskAssignDTO() { }
        public TaskAssignDTO(string technicianName, string taskID, int status)
        {
            TechnicianName = technicianName;
            Status = status;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TasksAssign, TaskAssignDTO>()
                .ForMember(dest => dest.TechnicianName, opt => opt.MapFrom(src => src.Technician.Name))
                .ReverseMap();
        }
    }
}
