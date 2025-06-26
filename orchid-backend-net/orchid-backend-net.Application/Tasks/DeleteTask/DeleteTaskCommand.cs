using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.Tasks.DeleteTask
{
    public class DeleteTaskCommand : IRequest<string>, ICommand
    {

        public string ID { get; set; }
        public DeleteTaskCommand() { }
        public DeleteTaskCommand(string ID)
        {
            this.ID = ID;
        }

    }
}
