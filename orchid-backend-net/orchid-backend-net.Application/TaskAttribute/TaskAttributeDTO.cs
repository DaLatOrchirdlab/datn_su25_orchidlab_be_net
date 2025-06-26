using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.TaskAttribute
{
    public class TaskAttributeDTO : IMapFrom<TaskAttributes>
    {
        public string ID { get; set; }
        //public string Name { get; set; }
        public string Description { get; set; }
        public string TaskID { get; set; }
        public double Value { get; set; }
        //public enum Unit
        public bool Status { get; set; }

        public TaskAttributeDTO(string iD, string description, string taskID, double value, bool status)
        {
            ID = iD;
            //Name = name;
            Description = description;
            TaskID = taskID;
            Value = value;
            Status = status;
        }
        public TaskAttributeDTO() { }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TaskAttributeDTO, TaskAttributes>();
        }
    }
}
