using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.User.GetUserInfor
{
    public class GetUserInforQuery : IRequest<UserDTO>, IQuery
    {
        public string ID { get; set; }
        public GetUserInforQuery(string id)
        {
            this.ID = id;
        }
        public GetUserInforQuery() { }

    }
}
