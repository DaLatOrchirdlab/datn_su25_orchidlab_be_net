using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.Tasks.CreateTask
{
    public class CreateTaskCommand : IRequest<string>, ICommand
    {
        public string Researcher { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Create_at { get; set; }
        public int Status { get; set; }
        public Domain.Enums.TaskStatus StatusEnum { get; set; }
        public List<TaskAttributes> Attribute { get; set; }
        public List<string> TechnicianID { get; set; }
        public CreateTaskCommand(string researcher, string name, string description, DateTime start_date, DateTime end_date, DateTime create_at, int status, List<TaskAttributes> attribute, List<string> technicianID, Domain.Enums.TaskStatus StatusEnum)
        {
            Researcher = researcher;
            Name = name;
            Description = description;
            Start_date = start_date;
            End_date = end_date;
            Create_at = create_at;
            Attribute = attribute;
            TechnicianID = technicianID;
            Status = status;
            this.StatusEnum = StatusEnum;
        }
        public CreateTaskCommand() { }
    }
}
