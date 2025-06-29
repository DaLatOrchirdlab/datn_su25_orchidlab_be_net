using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

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

    public class GetElementInforQueryHandler(IElementRepositoty elementRepositoty, IMapper mapper) : IRequestHandler<GetElementInforQuery, ElementDTO>
    {
        public async Task<ElementDTO> Handle(GetElementInforQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var element = await elementRepositoty.FindAsync(x => x.Status == true, cancellationToken);
                return element.MapToElementDTO(mapper);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
