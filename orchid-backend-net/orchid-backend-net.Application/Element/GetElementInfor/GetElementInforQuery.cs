using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Element.GetElementInfor
{
    public class GetElementInforQuery : IRequest<ElementDTO>, IQuery
    {
        public string ID {  get; set; }
        public GetElementInforQuery( string id)
        {
            this.ID = id;
        }
        public GetElementInforQuery() { }
    }
}
