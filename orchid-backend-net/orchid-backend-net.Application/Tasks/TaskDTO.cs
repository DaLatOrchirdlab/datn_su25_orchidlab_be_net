using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Application.TaskAssign;
using orchid_backend_net.Application.TaskAttribute;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.Tasks
{
    public class TaskDTO : IMapFrom<Domain.Entities.Tasks>
    {
        public string ID { get; set; }
        public string Researcher { get; set; }
        public List<TaskAssignDTO> AssignDTOs { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskAttributeDTO> AttributeDTOs { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Create_at { get; set; }
        public Domain.Enums.TaskStatus StatusEnum { get; set; }
        public int Status { get; set; }
        //public List<Domain.Entities.TaskAttribute> Attribute { get; set; }
        //public List<string> TechnicianID {  get; set; }

        public TaskDTO Create(string id, string researcher, string name, 
            string description, DateTime create_at, DateTime start_date, 
            DateTime end_date, int status, List<TaskAssignDTO> assign,
            List<TaskAttributeDTO> attribute)
        {
            var result = new TaskDTO
            {
                ID = id,
                Researcher = researcher,
                //TechnicianID = technicianID,
                Name = name,
                Description = description,
                Start_date = start_date,
                End_date = end_date,
                Create_at = create_at,
                Status = status,
                AttributeDTOs = attribute,
                AssignDTOs = assign
            };
            switch (status)
            {
                case 0:
                    {
                        result.StatusEnum = Domain.Enums.TaskStatus.Cancel;
                        break;
                    }
                case 1:
                    {
                        result.StatusEnum = Domain.Enums.TaskStatus.Assigned;
                        break;
                    }
                case 2:
                    {
                        result.StatusEnum = Domain.Enums.TaskStatus.Taken;
                        break;
                    }
                case 3:
                    {
                        result.StatusEnum = Domain.Enums.TaskStatus.InProcess;
                        break;
                    }
                case 4:
                    {
                        result.StatusEnum = Domain.Enums.TaskStatus.DoneInLate;
                        break;
                    }
                case 5:
                    {
                        result.StatusEnum = Domain.Enums.TaskStatus.DoneInTime;
                        break;
                    }
            }
            return result;
        }

        public TaskDTO CreateFullTask(string id, string researcher, string name, string description, DateTime create_at, DateTime start_date, DateTime end_date)
        {
            return new TaskDTO
            {
                ID = id,
                Researcher = researcher,
                //TechnicianID = technicianID,
                Name = name,
                Description = description,
                Start_date = start_date,
                End_date = end_date,
                Create_at = create_at,
                //Attribute = attribute
            };
        }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Tasks, TaskDTO>()
                .ForMember(dest => dest.AssignDTOs, opt => opt.MapFrom(src => src.Assigns))
                .ForMember(dest => dest.AttributeDTOs, opt => opt.MapFrom(src => src.Attributes));
        }
    }
}
