using AutoMapper;
using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Element.GetElementInfor
{
    public class GetElementInforQueryHandler(IElementRepositoty elementRepositoty, IMapper mapper) : IRequestHandler<GetElementInforQuery, ElementDTO>
    {
        private readonly IElementRepositoty _elementRepositoty;
        private readonly IMapper _mapper;
        public async Task<ElementDTO> Handle(GetElementInforQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var element = await this._elementRepositoty.FindAsync(x => x.Status == true, cancellationToken);
                if (element == null)
                    throw new NotFoundException($"Not found element with ID :{request.ID}");
                return element.MapToElementDTO(_mapper);
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
