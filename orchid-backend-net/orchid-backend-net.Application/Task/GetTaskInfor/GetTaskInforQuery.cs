using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Task.GetTaskInfor
{
    public class GetTaskInforQuery : IRequest<TaskDTO>, IQuery
    {
        public string ID {  get; set; }
        public GetTaskInforQuery(string ID)
        {
            this.ID = ID;
        }
    }
}
