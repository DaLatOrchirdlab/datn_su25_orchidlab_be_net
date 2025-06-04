using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
