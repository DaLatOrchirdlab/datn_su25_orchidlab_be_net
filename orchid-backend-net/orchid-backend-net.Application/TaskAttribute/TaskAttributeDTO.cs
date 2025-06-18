using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.TaskAttribute
{
    public class TaskAttributeDTO : IMapFrom<Domain.Entities.TaskAttribute>
    {
        public string ID {  get; set; }
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
            profile.CreateMap<TaskAttributeDTO, Domain.Entities.TaskAttribute>();
        }
    }
}
