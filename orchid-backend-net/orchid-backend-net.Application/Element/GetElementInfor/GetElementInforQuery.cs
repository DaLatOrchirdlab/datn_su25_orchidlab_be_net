using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.Element.GetElementInfor
{
    public class GetElementInforQuery : IRequest<ElementDTO>, IQuery
    {
        public string ID { get; set; }
        public GetElementInforQuery(string id)
        {
            this.ID = id;
        }
        public GetElementInforQuery() { }
    }
}
