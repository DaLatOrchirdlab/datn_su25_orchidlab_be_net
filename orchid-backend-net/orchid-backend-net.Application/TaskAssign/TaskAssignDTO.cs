using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.TaskAssign
{
    public class TaskAssignDTO : IMapFrom<Domain.Entities.TaskAssign>
    {
        public string TechnicianID { get; set; }
        public string TaskID { get; set; }
        public int Status { get; set; }

        public TaskAssignDTO() { }
        public TaskAssignDTO(string technicianID, string taskID, int status)
        {
            TechnicianID = technicianID;
            TaskID = taskID;
            Status = status;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TaskAssignDTO, Domain.Entities.TaskAssign>();
        }
    }
}
