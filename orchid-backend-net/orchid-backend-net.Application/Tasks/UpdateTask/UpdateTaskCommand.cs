using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.Tasks.UpdateTask
{
    public class UpdateTaskCommand : IRequest<string>, ICommand
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public UpdateTaskCommand(string id, string name, string description, int status)
        {
            ID = id;
            Name = name;
            Description = description;
            Status = status;
        }
    }
}
