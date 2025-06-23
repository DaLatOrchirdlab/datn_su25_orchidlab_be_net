using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method.GetMethodInfor
{
    public class GetMethodInforQuery : IRequest<MethodDTO>, IQuery
    {
        public string ID {  get; set; }
        public GetMethodInforQuery(string id)
        {
            ID = id;
        }
    }
}
