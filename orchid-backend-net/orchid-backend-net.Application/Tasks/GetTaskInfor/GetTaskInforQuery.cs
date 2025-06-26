using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.Tasks.GetTaskInfor
{
    public class GetTaskInforQuery : IRequest<TaskDTO>, IQuery
    {
        public string ID { get; set; }
        public GetTaskInforQuery(string ID)
        {
            this.ID = ID;
        }
    }
}
